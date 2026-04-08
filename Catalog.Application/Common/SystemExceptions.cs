using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Common
{
    public class SystemExceptions
    {
        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message)
            {
            }

            public BadRequestException(string[] errors) : base("Multiple errors occurred. See error details.")
            {
                Errors = errors;
            }

            public string[] Errors { get; set; }
        }
        public class ErrorMappingException : Exception
        {
            public ErrorMappingException() : base("Error was Happen while Mapping")
            {
            }
        }

        public class ValidationErrorException : Exception
        {
            public ValidationErrorException(string[] errors) : base(string.Join("; ", errors))
            {
                Errors = errors;
            }

            public string[] Errors { get; set; }
        }

        public class NoDataFoundException : Exception
        {
            public NoDataFoundException() : base("No Data Found")
            {
            }

            public NoDataFoundException(string message) : base(message) { }

        }
        public class SaveChangesFailedException : Exception
        {
            public SaveChangesFailedException() : base("Failed to save changes to the database.") { }


            public SaveChangesFailedException(string message) : base(message) { }

            public SaveChangesFailedException(string message, Exception innerException) : base(message, innerException) { }
        }
        public class EntityNotFoundException : Exception
        {
            public EntityNotFoundException() : base($"Data Not Found")
            {
            }

            public EntityNotFoundException(string message) : base(message)
            {
            }
        }
        public class SocketException : Exception
        {
            public SocketException(string message) : base(message)
            {
            }
        }
        public class AlreadyExistsException : Exception
        {
            public AlreadyExistsException(string message) : base($"{message} already exists.")
            {
            }

        }
        public class NotAuthorizedException : Exception
        {
            public NotAuthorizedException(string authority) : base($"You do not have the authoriy for '{authority}'")
            {
            }
        }
        public class InvalidRefreshTokenException : Exception
        {
            public InvalidRefreshTokenException() : base("Invalid Refresh Token")
            {
            }
        }
        public class UnauthenticatedException : Exception
        {
            public UnauthenticatedException() : base($"Username or password is incorrect")
            {
            }
        }
        public class UserNotFoundException : Exception
        {
            public UserNotFoundException() : base($"Username not found!")
            {
            }
        }
        public class ConflictException : Exception
        {
            public ConflictException(string message) : base(message)
            {
            }
        }
        public class HtmlGenerationException : Exception
        {
            public HtmlGenerationException(string message) : base(message)
            {
            }
        }
        public class ValidationException : Exception
        {
            public ValidationException(string message) : base(message) { }

            public ValidationException(string[] errors) : base(string.Join("; ", errors))
            {
                Errors = errors;
            }
            public string[] Errors { get; set; }

        }
    }
}
