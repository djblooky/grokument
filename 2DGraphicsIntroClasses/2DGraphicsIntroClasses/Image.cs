using System;
using System.IO;

class Image
{
    /// <summary>
    /// An array of bytes to store a buffer
    /// </summary>
	internal byte [] buffer;
    /// <summary>
    /// An int to store screen width
    /// </summary>
	public int Width { get; }
    /// <summary>
    /// An int to store screen height
    /// </summary>
	public int Height { get; }
    /// <summary>
    /// An instance of format used to retrieve grayscale, bgr, and bgre specifications
    /// </summary>
	public Format Format { get; }

    /// <summary>
    /// An integer value that returns the number of bytes per row given a particular screen width and format
    /// </summary>
	public int BytesPerRow {
		get {
			return Width * (int)Format;
		}
	}

    /// <summary>
    /// The constructor for Image.cs which takes in a width, height and format and assigns them to local variables. 
    /// A buffer is created from a new byte by multiplying screen height and the local bytes per row
    /// </summary>
    /// <param name="width">Screen width is passed in to assign local Width int</param>
    /// <param name="height">The screen height is passed in to assign local Height int</param>
    /// <param name="format">An instance of format is passed in to assign the local Format</param>
	public Image (int width, int height, Format format)
	{
		Width = width;
		Height = height;
		Format = format;

		buffer = new byte [height * BytesPerRow];
	}

    /// <summary>
    /// Var bpp id assigned from the local format cast as an int, and then multiplied by the Width to calculate bytesPerLine
    /// Var half is assigned to the current Height shifted right by 1 byte
    /// A for loop will iterate until it loops as many times as var half. This will assign l1 and l2 vars to be equal to the 
    /// current number of bytes passed depending on the current line (iterator l). A nested for loop will go through each pixel
    /// in each line and reverse their positions from l1 and l2.
    /// </summary>
	public void VerticalFlip ()
	{
		var bpp = (int)Format;
		int bytesPerLine = Width * bpp;

		var half = Height >> 1;
		for (int l = 0; l < half; l++) {
			var l1 = l * bytesPerLine;
			var l2 = (Height - 1 - l) * bytesPerLine;

			for (int i = 0; i < bytesPerLine; i++) {
				byte pixel = buffer [l1 + i];
				buffer [l1 + i] = buffer [l2 + i];
				buffer [l2 + i] = pixel;
			}
		}
	}

    /// <summary>
    /// A for loop iterates through the entire buffer length and clears each index of the array to zero
    /// </summary>
	public void Clear ()
	{
		for (int i = 0; i < buffer.Length; i++)
			buffer [i] = 0;
	}

    /// <summary>
    /// If the coordinates are within the screen range, gets the offset, length, and sets an empty value.
    /// For each character in the RGB color code, shift the value left by 8 bytes and add to the buffer offset.
    /// Can get or set the offset, value, and length(format)
    /// </summary>
    /// <param name="x">An x coordinate in pixels</param>
    /// <param name="y">A y coordinate in pixels</param>
    /// <returns>A new color of a particular value and format</returns>
	public Color this [int x, int y] {
		get {
			if (x < 0 || x >= Width) throw new ArgumentException ("x");
			if (y < 0 || y >= Height) throw new ArgumentException ("y");

			var offset = GetOffset (x, y);
			var len = (int)Format;
			int value = 0;
			for (var ch = 0; ch < 4; ch++)
				value = (value << 8) | (ch < len ? buffer [offset++] : 0xFF);

			return new Color (value, Format);
		}
		set {
			if (x < 0 || x >= Width) return; //throw new ArgumentException ($"{nameof(x)}={x} {nameof(Width)}={Width}");
			if (y < 0 || y >= Height) return; // throw new ArgumentException ($"{nameof(y)}={y} {nameof(Height)}={Height}");

			var offset = GetOffset (x, y);
			var v = value.value;
			var len = (int)Format;
			for (int ch = 0; ch < len; ch++)                   // 0123
				buffer [offset++] = (byte)(v >> (3 - ch) * 8); // BGRA
		}
	}

    /// <summary>
    /// Takes in an x and y coordinate and determines an offset based on the current format and bytes per row.
    /// </summary>
    /// <param name="x">An x cordinate in pixels</param>
    /// <param name="y">A y coordinate in pixels</param>
    /// <returns>Y multiplied by bytes per row times format cast as an int</returns>
	int GetOffset (int x, int y)
	{
		return y * BytesPerRow + x * (int)Format;
	}

    /// <summary>
    /// Creates a new header file and binary writer at the specified path, then writes the header to the writer.
    /// Writes the buffer if there is no RLE data, otherwise unloads the Rle data
    /// </summary>
    /// <param name="path">File path string to save the writer and header files to</param>
    /// <param name="rle">Boolean storing whether there is data compression encoding or not</param>
    /// <returns>Returns true</returns>
	public bool WriteToFile (string path, bool rle = true)
	{
		var bpp = (int)Format;
		using (var writer = new BinaryWriter (File.Create (path))) {
			var header = new TGAHeader {
				IdLength = 0, // The IDLength set to 0 indicates that there is no image identification field in the TGA file
				ColorMapType = 0, // a value of 0 indicates that no palette is included
				BitsPerPixel = (byte)(bpp * 8),
				Width = (short)Width,
				Height = (short)Height,
				DataTypeCode = DataTypeFor (bpp, rle),
				ImageDescriptor = (byte)(0x20 | (Format == Format.BGRA ? 8 : 0)) // top-left origin
			};
			WriteTo (writer, header);
			if (!rle)
				writer.Write (buffer);
			else
				UnloadRleData (writer);
		}
		return true;
	}

    /// <summary>
    /// Loads an image to a specified file path using a new BinaryReader Creates a header from the binary reader and 
    /// uses those specifications to create height, width, bytespp, and format variables.
    /// Ensures that the format and image size is correct, and then uncompresses depending on the type of image
    /// specified in the header file. Vertical flips images of a certain descriptor, and returns the image.
    /// </summary>
    /// <param name="path">A string containing a specified file path</param>
    /// <returns>Returns an Image</returns>
	public static Image Load (string path)
	{
		using (var reader = new BinaryReader (File.OpenRead (path))) {
			var header = ReadHeader (reader);

			var height = header.Height;
			var width = header.Width;
			var bytespp = header.BitsPerPixel >> 3;
			var format = (Format)bytespp;

			if (width <= 0 || height <= 0)
				throw new InvalidProgramException ($"bad image size: width={width} height={height}");
			if (format != Format.BGR && format != Format.BGRA && format != Format.GRAYSCALE)
				throw new InvalidProgramException ($"unknown format {format}");

			var img = new Image (width, height, format);

			switch (header.DataTypeCode) {
			case DataType.UncompressedTrueColorImage:
			case DataType.UncompressedBlackAndWhiteImage:
				reader.Read (img.buffer, 0, img.buffer.Length);
				break;
			case DataType.RleTrueColorImage:
			case DataType.RleBlackAndWhiteImage:
				img.LoadRleData (reader);
				break;
			default:
				throw new InvalidProgramException ($"unsupported image format {header.DataTypeCode}");
			}

			if ((header.ImageDescriptor & 0x20) == 0)
				img.VerticalFlip ();

			return img;
		}
	}

	/// <summary>
	/// This is a function that writes the parameter header to an instance of writer when called. It returns void
	/// </summary>
	/// <param name="writer">passed into the function when called, it is written to using data from the header variable</param>
	/// <param name="header">parameter passed into the function that contains data that is used to add information to the writer variable</param>
	static void WriteTo (BinaryWriter writer, TGAHeader header)
	{
		writer.Write (header.IdLength);
		writer.Write (header.ColorMapType);
		writer.Write ((byte)header.DataTypeCode);
		writer.Write (header.ColorMapOrigin);
		writer.Write (header.ColorMapLength);
		writer.Write (header.ColorMapDepth);
		writer.Write (header.OriginX);
		writer.Write (header.OriginY);
		writer.Write (header.Width);
		writer.Write (header.Height);
		writer.Write (header.BitsPerPixel);
		writer.Write (header.ImageDescriptor);
	}

	/// <summary>
	/// This function returns a value of type TGAHeader, it is assigned to the variable header in the function Image
	/// </summary>
	/// <param name="reader">passed into the function when called it is used to add data to the variable header</param>
	/// <returns name="header">variable declared in the function it is returned, contains data from reader variable that was added in </returns>
	static TGAHeader ReadHeader (BinaryReader reader)
	{
		/// <summary>
		/// header is set based on the data passed into the function from reader
		/// </summary>
		/// <value></value>
		var header = new TGAHeader {
			IdLength = reader.ReadByte (),
			ColorMapType = reader.ReadByte (),
			DataTypeCode = (DataType)reader.ReadByte (),
			ColorMapOrigin = reader.ReadInt16 (),
			ColorMapLength = reader.ReadInt16 (),
			ColorMapDepth = reader.ReadByte (),
			OriginX = reader.ReadInt16 (),
			OriginY = reader.ReadInt16 (),
			Width = reader.ReadInt16 (),
			Height = reader.ReadInt16 (),
			BitsPerPixel = reader.ReadByte (),
			ImageDescriptor = reader.ReadByte ()
		};
		return header;
	}

	/// <summary>
	/// Returns a true value when finished unloading data from rle 
	/// writes the length of writer every tick if curpix is larger than npixels, unloads data and returns true when all data is unloaded
	/// </summary>
	/// <param name="writer">passed into the function, is written to while function is running </param>
	/// <returns>true</returns>
	bool UnloadRleData (BinaryWriter writer)
	{
		/// <summary>
		/// the maximum size a chunk of data can be
		/// </summary>
		const int max_chunk_length = 128;

		/// <summary>
		/// width and height of an image
		/// </summary>
		int npixels = Width * Height;

		/// <summary>
		/// the current pixel the function is unloading
		/// </summary>
		int curpix = 0;

		/// <summary>
		/// bytes per pixel is set to the type of image (greyscale or color) and is used to determine how large the file is
		/// </summary>
		/// <returns></returns>
		var bpp = (int)Format;

		while (curpix < npixels) {
			int chunkstart = curpix * bpp;
			int curbyte = curpix * bpp;
			int run_length = 1;
			bool literal = true;
			while (curpix + run_length < npixels && run_length < max_chunk_length && curpix + run_length < curpix + Width) {
				bool succ_eq = true;
				for (int t = 0; succ_eq && t < bpp; t++)
					succ_eq = (buffer [curbyte + t] == buffer [curbyte + t + bpp]);
				curbyte += bpp;
				if (1 == run_length)
					literal = !succ_eq;
				if (literal && succ_eq) {
					run_length--;
					break;
				}
				if (!literal && !succ_eq)
					break;
				run_length++;
			}
			curpix += run_length;

			writer.Write ((byte)(literal ? run_length - 1 : 128 + (run_length - 1)));
			writer.Write (buffer, chunkstart, literal ? run_length * bpp : bpp);
		}
		return true;
	}

	/// <summary>
	/// This functiom takes parameter reader and uses it to count how many pixels are in reader, if the current pixel count is smaller than the total number of pixels it continues running in a while loop
	/// </summary>
	/// <param name="reader">passed in when called its size is used to calculate the size of the image</param>
	void LoadRleData (BinaryReader reader)
	{
		/// <summary>
		/// pixelcount is the combined value of width and height
		/// </summary>
		var pixelcount = Width * Height;
		/// <summary>
		/// currentpixel is incremented each loop as the function checks each pixel
		/// </summary>
		var currentpixel = 0;
		/// <summary>
		/// currentbyte is incremented each cycle if it is less than bytesapp
		/// </summary>
		var currentbyte = 0;

		/// <summary>
		/// bytes per pixel is set to the type of image (greyscale or color) and is used to determine how large the file is
		/// </summary>
		/// <returns></returns>
		var bytespp = (int)Format;

		/// <summary>
		/// The byte at which the reader stars reading if the image is not greyscale
		/// </summary>
		var color = new byte [4];

		do {
			/// <summary>
			/// chunkheader is used as a count of how large the reader variable is within the Do loop
			/// </summary>
			var chunkheader = reader.ReadByte ();
			if (chunkheader < 128) {
				chunkheader++;
				for (int i = 0; i < chunkheader; i++) {
					for (int t = 0; t < bytespp; t++)
						buffer [currentbyte++] = reader.ReadByte ();
					currentpixel++;
					if (currentpixel > pixelcount)
						throw new InvalidProgramException ("Too many pixels read");
				}
			} else {
				chunkheader -= 127;
				reader.Read (color, 0, bytespp);
				for (int i = 0; i < chunkheader; i++) {
					for (int t = 0; t < bytespp; t++)
						buffer [currentbyte++] = color [t];
					currentpixel++;
					if (currentpixel > pixelcount)
						throw new InvalidProgramException ("Too many pixels read");
				}
			}
		} while (currentpixel < pixelcount);
	}

	/// <summary>
	/// returns rle and sets it based on the value of bpp whether it is grayscale or color
	/// </summary>
	/// <param name="bpp">parameter that is passed in when the function is called, it is used to set the value of format</param>
	/// <param name="rle">returned value based on whether the bpp parameters is determined to be grayscale or color</param>
	/// <returns name="rle">rle is different depending on whether the image is greyscale or in color</returns>
	static DataType DataTypeFor (int bpp, bool rle)
	{
		/// <summary>
		/// variable that is used to determine if image is greyscale or color
		/// </summary>
		var format = (Format)bpp;

		if (format == Format.GRAYSCALE)
			return rle ? DataType.RleBlackAndWhiteImage : DataType.UncompressedBlackAndWhiteImage;

		return rle ? DataType.RleTrueColorImage : DataType.UncompressedTrueColorImage;
	}
}