using System;

namespace AlgorithmLib {
    public class ArrayHelper {

        /// <summary>
        /// 数组元素位移，子数组位移
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">要位移的子数组的起始位置</param>
        /// <param name="length">要位移的子数组的长度</param>
        /// <param name="offset">位移量，正数表示向后移，负数表示向前移</param>
        /// <remarks>
        /// 越界会抛错，比如[1,2,3,4,5,6,7]，我要[5,6]向后位移2，则抛错
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException">越界异常</exception>
        public static void ShiftArrayItems<T>(T[] array, int index, int length, int offset) {
            if (index < 0) {
                throw new IndexOutOfRangeException("index不能小于0");
            }
            if (index + length > array.Length) {
                throw new IndexOutOfRangeException("指定的子数组越界");
            }
            if (index + offset < 0 || index + length + offset > array.Length) {
                throw new IndexOutOfRangeException("子数组位移越界");
            }
            if (length == 0 || offset == 0) {
                return;
            }

            //将子数组Copy出来
            T[] tempArray = new T[length];
            Array.Copy(array, index, tempArray, 0, length);

            if (offset > 0) {
                
                Array.Copy(array, index + length, array, index, offset);
                Array.Copy(tempArray, 0, array, index + offset, length);
            }
            else {
                Array.Copy(array, index + offset, array, index + offset + length, -offset);
                Array.Copy(tempArray, 0, array, index + offset, length);
            }
        }
    }
}
