using System;
using System.Collections.Generic;

namespace DocumentosOnlineAPI.Exceptions {
    public class AccessDeniedException : DocumentosApiException {

        private const string DEFAULT_MESSAGE = "No se cuentan con permisos para acceder al documento solicitado";

        public AccessDeniedException() : base(DEFAULT_MESSAGE) {}

        public AccessDeniedException(string message) : base(message) {}

        public AccessDeniedException(Exception cause) : base(DEFAULT_MESSAGE,cause) {}

        public AccessDeniedException(string message, Exception cause) : base(message,cause) {}

    }
}