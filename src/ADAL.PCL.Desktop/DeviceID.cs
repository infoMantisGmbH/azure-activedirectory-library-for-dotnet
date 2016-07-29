//----------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    /// <summary>
    /// Summary description for DeviceID.
    /// </summary>
    public class DeviceID
    {

        internal static string strDeviceID { get; set; }

        public DeviceID()
        {
            strDeviceID = GetDeviceID();
        }


        private static Int32 METHOD_BUFFERED = 0;
        private static Int32 FILE_ANY_ACCESS = 0;
        private static Int32 FILE_DEVICE_HAL = 0x00000101;

        private const Int32 ERROR_NOT_SUPPORTED = 0x32;
        private const Int32 ERROR_INSUFFICIENT_BUFFER = 0x7A;

        private static Int32 IOCTL_HAL_GET_DEVICEID =
            ((FILE_DEVICE_HAL) << 16) | ((FILE_ANY_ACCESS) << 14)
            | ((21) << 2) | (METHOD_BUFFERED);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern bool KernelIoControl(Int32 dwIoControlCode,
            IntPtr lpInBuf, Int32 nInBufSize, byte[] lpOutBuf,
            Int32 nOutBufSize, ref Int32 lpBytesReturned);

        internal static string GetDeviceID()
        {
            // Initialize the output buffer to the size of a 
            // Win32 DEVICE_ID structure.
            byte[] outbuff = new byte[20];
            Int32 dwOutBytes;
            bool done = false;

            Int32 nBuffSize = outbuff.Length;

            // Set DEVICEID.dwSize to size of buffer.  Some platforms look at
            // this field rather than the nOutBufSize param of KernelIoControl
            // when determining if the buffer is large enough.
            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);
            dwOutBytes = 0;

            // Loop until the device ID is retrieved or an error occurs.
            while (!done)
            {
                if (KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero,
                    0, outbuff, nBuffSize, ref dwOutBytes))
                {
                    done = true;
                }
                else
                {
                    int error = Marshal.GetLastWin32Error();
                    switch (error)
                    {
                        case ERROR_NOT_SUPPORTED:
                            throw new NotSupportedException(
                                "IOCTL_HAL_GET_DEVICEID is not supported on this device",
                                new Win32Exception(error));

                        case ERROR_INSUFFICIENT_BUFFER:

                            // The buffer is not big enough for the data.  The
                            // required size is in the first 4 bytes of the output
                            // buffer (DEVICE_ID.dwSize).
                            nBuffSize = BitConverter.ToInt32(outbuff, 0);
                            outbuff = new byte[nBuffSize];

                            // Set DEVICEID.dwSize to size of buffer.  Some
                            // platforms look at this field rather than the
                            // nOutBufSize param of KernelIoControl when
                            // determining if the buffer is large enough.
                            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);
                            break;

                        default:
                            throw new Win32Exception(error, "Unexpected error");
                    }
                }
            }

            // Copy the elements of the DEVICE_ID structure.
            Int32 dwPresetIDOffset = BitConverter.ToInt32(outbuff, 0x4);
            Int32 dwPresetIDSize = BitConverter.ToInt32(outbuff, 0x8);
            Int32 dwPlatformIDOffset = BitConverter.ToInt32(outbuff, 0xc);
            Int32 dwPlatformIDSize = BitConverter.ToInt32(outbuff, 0x10);
            StringBuilder sb = new StringBuilder();

            for (int i = dwPresetIDOffset;
                i < dwPresetIDOffset + dwPresetIDSize; i++)
            {
                sb.Append(String.Format("{0:X2}", outbuff[i]));
            }

            sb.Append("-");

            for (int i = dwPlatformIDOffset;
                i < dwPlatformIDOffset + dwPlatformIDSize; i++)
            {
                sb.Append(String.Format("{0:X2}", outbuff[i]));
            }
            return sb.ToString();
        }
    }
}
