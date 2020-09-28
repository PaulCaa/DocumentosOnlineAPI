using System;
using System.Collections.Generic;

namespace DocumentosOnlineAPI.Exceptions {
    public class DocumentosDatabaseException : DocumentosApiException {

        private const string DEFAULT_MESSAGE = "Error al ejecutar operacion con la base de datos";

        public DocumentosDatabaseException() : base(DEFAULT_MESSAGE) {}

        public DocumentosDatabaseException(string message) : base(message) {}

        public DocumentosDatabaseException(Exception cause) : base(DEFAULT_MESSAGE,cause) {}

        public DocumentosDatabaseException(string message, Exception cause) : base(message,cause) {}

    }
}