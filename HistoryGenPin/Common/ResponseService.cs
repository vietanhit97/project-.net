using System;

namespace HistoryGenPin.Common
{
    public class ResponseService<T>
    {
        public bool status { get; private set; }
        public string message { get; private set; }
        public T data { get; private set; }
        public Exception exception { get; set; }

        public ResponseService(T data)
        {
            this.status = true;
            this.message = string.Empty;
            this.data = data;
        }
        public ResponseService()
        {
            this.status = false;
            this.message = string.Empty;
        }

        public ResponseService(Exception ex)
        {
            this.status = false;
            this.message = ex.Message;
            this.data = default;
            this.exception = ex;
        }

        public ResponseService(bool status, string message, T data)
        {
            this.status = status;
            this.message = message;
            this.data = data;
        }
    }
}
