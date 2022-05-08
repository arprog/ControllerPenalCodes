namespace ControllerPenalCodes.Shared
{
	public class Response<T>
	{
		public bool Ok { get; set; }

		public string Message { get; set; }

		public T Return { get; set; }

		public static Response<T> ResponseService(bool _ok, string _message, T _return)
		{
			return new Response<T>
			{
				Ok = _ok,
				Message = _message,
				Return = _return
			};
		}

		public static Response<T> ResponseService(bool _ok, string _message)
		{
			return new Response<T>
			{
				Ok = _ok,
				Message = _message
			};
		}

		public static Response<T> ResponseService(bool _ok, T _return)
		{
			return new Response<T>
			{
				Ok = _ok,
				Return = _return
			};
		}

		public static Response<T> ResponseService(bool _ok)
		{
			return new Response<T>
			{
				Ok = _ok
			};
		}
	}
}
