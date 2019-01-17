using System;
using System.Runtime.InteropServices;

namespace ggdotnet
{
	class Greengrass
	{

		[StructLayout( LayoutKind.Sequential )]
		unsafe public struct gg_lambda_context {
			[MarshalAs(UnmanagedType.LPStr)] public string function_arn;
			[MarshalAs(UnmanagedType.LPStr)] public string client_context;
		};

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)] 
		public delegate void Callback(ref gg_lambda_context ctx);

		[DllImport("libaws-greengrass-core-sdk-c.so", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
		public static extern int gg_global_init(int opt);

		[DllImport("libaws-greengrass-core-sdk-c.so", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
		public static extern int gg_log(int log, string format, string param);
		[DllImport("libaws-greengrass-core-sdk-c.so",  CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
		public static extern int gg_runtime_start(Callback handler, int opt);

	//	[DllImport("libaws-greengrass-core-sdk-c.so", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
	//	wUnsafeBlocks>true</AllowUnsafeBlocks>

	}	

	class Program
	{
		static void Main(string[] args)
		{
			int err;
			// Console.WriteLine("Hello World!");
			err = Greengrass.gg_global_init(0);			// ASYNC
			if (err != 0) {
				Greengrass.gg_log(1,"Hello %s", "from .Net");
			}

			Greengrass.Callback handler = (ref Greengrass.gg_lambda_context context) => {
				Console.WriteLine(String.Format("ARN: {0}",context.function_arn));
				Console.WriteLine(String.Format("Context: {0}",context.client_context));
			};
			Greengrass.gg_runtime_start(handler,1);

		}
	}
}
