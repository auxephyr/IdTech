using System;
using System.Diagnostics;

namespace Auxephyr.IdTech.Infrastructure
{
    [DebuggerStepThrough]
    internal static class SpanStream
    {
        public static byte ReadByte(ReadOnlySpan<byte> span, ref int offset)
        {
            var value = span[offset];
            offset++;
            return value;
        }

        public static short ReadInt16(ReadOnlySpan<byte> span, ref int offset)
        {
            int value = span[offset++];
            value |= span[offset++] << 8;
            
            return unchecked((short) value);
        }

        public static byte[] ReadBytes(ReadOnlySpan<byte> span, ref int offset, int length)
        {
            var buffer = new byte[length];
            span.Slice(offset, length).CopyTo(buffer);
            offset += length;
            return buffer;
        }

        public static void Write(Span<byte> span, ref int offset, byte value)
        {
            span[offset++] = value;
        }

        public static void Write(Span<byte> span, ref int offset, short value)
        {
            span[offset++] = unchecked((byte) value);
            span[offset++] = unchecked((byte) (value >> 8));
        }

        public static void Write(Span<byte> span, ref int offset, ReadOnlySpan<byte> value)
        {
            value.CopyTo(span.Slice(offset, value.Length));
            offset += value.Length;
        }
    }
}