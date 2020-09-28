using System;
using System.Collections.Generic;

namespace DocumentosOnlineAPI.Exceptions {
    public class DocumentosApiException : ApplicationException {

        private const string DEFAULT_MESSAGE = "Se produjo un error interno";

        public DocumentosApiException() : base(DEFAULT_MESSAGE) {}

        public DocumentosApiException(string message) : base(message) {}

        public DocumentosApiException(Exception cause) : base(DEFAULT_MESSAGE,cause) {}

        public DocumentosApiException(string message, Exception cause) : base(message,cause) {}

    }
}