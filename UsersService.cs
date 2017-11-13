using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Grpctest.Messages;
using static Grpctest.Messages.UserService;

namespace Grpctest
{
    public class UsersService : UserServiceBase
    {
        public override async Task<UserResponse> GetByUserId(GetByUserIdRequest request, ServerCallContext context)
        {
            Metadata md = context.RequestHeaders;

            foreach (var entry in md)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }

            foreach (var u in Users.users)
            {
                if (request.UserId == u.Id)
                {
                    return new UserResponse
                    {
                        User = u
                    };
                }
            }
            throw new Exception($"User not found with id {request.UserId}");
        }

        public override async Task GetAll(GetAllRequest request, IServerStreamWriter<UserResponse> responseStream, ServerCallContext context)
        {
            foreach (var u in Users.users)
            {
                await responseStream.WriteAsync(new UserResponse()
                {
                    User = u
                });
            }
        }

        public override async Task<AddImageResponse> AddImage(IAsyncStreamReader<AddImageRequest> requestStream, ServerCallContext context)
        {
            Metadata md = context.RequestHeaders;

            foreach (var entry in md)
            {
                if (entry.Key.Equals("UserId", StringComparison.CurrentCultureIgnoreCase))
                {
                    System.Console.WriteLine($"Receiving image for UserId: {entry.Value}");
                    break;
                }
            }
            var data = new List<byte>();

            while (await requestStream.MoveNext())
            {
                System.Console.WriteLine($"Received {requestStream.Current.Data.Length} bytes");
                data.AddRange(requestStream.Current.Data);
            }
            System.Console.WriteLine($"Received file with {data.Count} bytes");
            return new AddImageResponse()
            {
                IsOk = true
            };
        }

        public override async Task SaveAll(IAsyncStreamReader<UserRequest> requestStream, IServerStreamWriter<UserResponse> responseStream, ServerCallContext context){
            while (await requestStream.MoveNext())
            {
                var user = requestStream.Current.User;

                lock (this)
                {
                    Users.users.Add(user);
                }

                await responseStream.WriteAsync(new UserResponse{
                    User = user
                });
            }

            System.Console.WriteLine("Users");
            foreach (var u in Users.users)
            {
                System.Console.WriteLine(u);
            }
        }
    }
}