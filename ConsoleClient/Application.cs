using RocheApp.Domain.Models;
using RocheApp.Domain.Services.User;
using RocheApp.Domain.Services.User.Interfaces;
using System;

namespace ConsoleClient
{
    public class Application
    {
        private readonly IUserService _userService;
        private readonly IUserUpdater _userUpdater;
        private readonly IUserCreator _userCreator;

        public Application(IUserService userService, IUserUpdater userUpdater, IUserCreator userCreator)
        {
            _userService = userService;
            _userUpdater = userUpdater;
            _userCreator = userCreator;
        }
        
        public void Run()
        {
            var option = -1;

            while (option != 0)
            {
                Console.Clear();
                Console.WriteLine("----------------------------");
                Console.WriteLine("Hello, please select option.\r");
                Console.WriteLine("Approve selection with ENTER.\r");
                Console.WriteLine("Empty input is NULL\r");
                Console.WriteLine("Input validation is not implemented :)\r");
                Console.WriteLine("1) Search");
                Console.WriteLine("2) Update");
                Console.WriteLine("3) Create");
                Console.WriteLine("0) Exit");
                Console.WriteLine("----------------------------\n");    
                
                var line = Console.ReadLine();
                if(!int.TryParse(line, out option)) continue;

                switch (option)
                {
                    case 1:
                        Search();
                        PressAnyKey();
                        break;
                    case 2:
                        Update();
                        PressAnyKey();
                        break;
                    case 3:
                        Create();
                        PressAnyKey();
                        break;
                }
                
            }
            
            

            var userServiceResult = _userService.Users(UserFilter.EmptyFilter);
            Console.WriteLine($"{userServiceResult.TotalUserCount} {userServiceResult.TotalPetCount}");

            // using var scope = _serviceProvider.CreateScope();
            //
            // var userService = scope.ServiceProvider.GetService<IUserService>();
            // // var res = userService.Users(UserFilter.EmptyFilter);
            // var res = userService.Users(new UserFilter {FirstName = "ian", Status = 0});
            // Console.WriteLine($"{res.TotalUserCount} {res.TotalPetCount}");
            //
            // var userUpdater = scope.ServiceProvider.GetService<IUserUpdater>();
            // var updaterResult = userUpdater.Update(3).ToList();
            // Console.WriteLine(updaterResult.First().ExperiencePoints);
            //
            // var userCreator = scope.ServiceProvider.GetService<IUserCreator>();
            // var creatorResult = userCreator.Create(new User
            //     {FirstName = "Tom", LastName = "Ron", Status = 1, ExperiencePoints = 1, PetsDeleted = 0});
            // Console.WriteLine($"{creatorResult.UserId} {creatorResult.RowVersion}");
        }

        private void PressAnyKey()
        {
            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }
        private void Search()
        {
            Console.WriteLine("!!! SEARCH !!!");
            Console.WriteLine("Enter FirstName:");
            var firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName))
                firstName = null;
            Console.WriteLine("Enter Status:");
            var statusText = Console.ReadLine();
            byte? status = null;
            if(!string.IsNullOrWhiteSpace(statusText))
                status = byte.Parse(statusText);
            var result = _userService.Users(new UserFilter {FirstName = firstName, Status = status});
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Number of users: {result.TotalUserCount}, number of pets: {result.TotalPetCount}");
            Console.ResetColor();
            foreach (var user in result.Users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName} - ExperiencePoints: {user.ExperiencePoints}, PetsDeleted: {user.PetsDeleted}, Status: {user.Status}, RowVersion: {BitConverter.ToString(user.RowVersion)}");
                foreach (var pet in user.Pets)
                {
                    Console.WriteLine($"    {pet.Name}");
                }
            }
        }

        private void Update()
        {
            Console.WriteLine("!!! UPDATE !!!");
            Console.WriteLine("Enter iterations count:");
            var iterations = int.Parse(Console.ReadLine());
            var result = _userUpdater.Update(iterations);
            foreach (var item in result)
            {
                Console.WriteLine($"ExperiencePoints: {item.ExperiencePoints} RowVersion: {BitConverter.ToString(item.RowVersion)}");
            }
        }

        private void Create()
        {
            Console.WriteLine("!!! CREATE !!!");
            var user = new User();
            Console.WriteLine("Enter FirstName:");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("Enter LastName:");
            user.LastName = Console.ReadLine();
            Console.WriteLine("Enter Status:");
            user.Status = byte.Parse(Console.ReadLine());
            Console.WriteLine("Enter ExperiencePoints:");
            user.ExperiencePoints = int.Parse(Console.ReadLine());
            var result = _userCreator.Create(user);
            Console.WriteLine($"UserId: {result.UserId} RowVersion: {BitConverter.ToString(result.RowVersion)}");
        }
    }
}