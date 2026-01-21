using System.Text.RegularExpressions;

namespace UntisAPI.ResourceTypes
{
    public struct Color
    {
        public uint R;
        public uint G;
        public uint B;
        public uint A;

        public Color(uint r, uint g, uint b, uint a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color(uint r, uint g, uint b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }

        public static Color FromHex(string hexCode)
        {
            if (string.IsNullOrWhiteSpace(hexCode))
            {
                throw new ArgumentException("Hex code cannot be null or empty.", nameof(hexCode));
            }

            hexCode = hexCode.TrimStart('#');

            if (hexCode.Length != 6 && hexCode.Length != 8)
            {
                throw new ArgumentException(
                    $"Hex code must be 6 (RGB) or 8 (ARGB) characters long, but was {hexCode.Length}.",
                    nameof(hexCode)
                );
            }

            if (!Regex.IsMatch(hexCode, "^[0-9A-Fa-f]+$"))
            {
                throw new ArgumentException(
                    "Hex code contains invalid characters. Only 0-9, A-F are permitted.",
                    nameof(hexCode)
                );
            }

            try
            {
                byte a,
                    r,
                    g,
                    b;

                if (hexCode.Length == 8)
                {
                    a = Convert.ToByte(hexCode[..2], 16);
                    r = Convert.ToByte(hexCode.Substring(2, 2), 16);
                    g = Convert.ToByte(hexCode.Substring(4, 2), 16);
                    b = Convert.ToByte(hexCode.Substring(6, 2), 16);
                }
                else
                {
                    a = 255;
                    r = Convert.ToByte(hexCode[..2], 16);
                    g = Convert.ToByte(hexCode.Substring(2, 2), 16);
                    b = Convert.ToByte(hexCode.Substring(4, 2), 16);
                }

                return new Color(a, r, g, b);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Failed to parse hex code.", nameof(hexCode), ex);
            }
            catch (OverflowException ex)
            {
                throw new ArgumentException("Hex value exceeds byte range.", nameof(hexCode), ex);
            }
        }

        public override readonly string ToString() => $"Color(A={A}, R={R}, G={G}, B={B})";
    }
}
