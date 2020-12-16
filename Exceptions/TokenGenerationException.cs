using System;
using System.Collections.Generic;

namespace DocumentosOnlineAPI.Exceptions {
    public class TokenGenerationException : DocumentosApiException {

        private const string DEFAULT_MESSAGE = "Se produjo un error en la generacion de token";

        public TokenGenerationException() : base(DEFAULT_MESSAGE) {}

        public TokenGenerationException(string message) : base(message) {}

        public TokenGenerationException(Exception cause) : base(DEFAULT_MESSAGE,cause) {}

        public TokenGenerationException(string message, Exception cause) : base(message,cause) {}

    }
}