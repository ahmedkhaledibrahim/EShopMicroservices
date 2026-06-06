using FluentValidation;
using FluentValidation.Results;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Discount.gRPC.Interceptors
{
    public class ValidationInterceptor : Interceptor
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var validator = _serviceProvider.GetService<IValidator<TRequest>>();
            if (validator != null) {
                ValidationResult result = await validator.ValidateAsync(request);

                if (!result.IsValid)
                {
                    throw new RpcException(
                        new Status(
                            StatusCode.InvalidArgument,
                            string.Join("; ",
                                result.Errors.Select(x => x.ErrorMessage))));
                }
            }
            return await continuation(request, context);
        }
    }
}
