using System;
using System.Collections.Generic;

namespace DocumentosOnlineAPI.Exceptions {
    public class WrongInputException : DocumentosApiException {

        private const string DEFAULT_MESSAGE = "Faltan datos de entrada necesarios para operacion";

        public WrongInputException() : base(DEFAULT_MESSAGE) {}

        public WrongInputException(string Message) : base(Message) {}

    }
}