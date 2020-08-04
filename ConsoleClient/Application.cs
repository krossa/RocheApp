using RocheApp.Domain.Models;
using RocheApp.Domain.Services.User;
using RocheApp.Domain.Services.User.Interfaces;
using System;
using System.Threading.Tasks;

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

        public async Task Run()
        {
            do
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
                if (!int.TryParse(line, out var option)) continue;

                switch (option)
                {
                    case 1:
                        await Search();
                        break;
                    case 2:
                        await Update();
                        break;
                    case 3:
                        await Create();
                        break;
                    case 0:
                        Goodbye();
                        return;
                }
            } while (true);
        }

        private void PressAnyKey()
        {
            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }

        private void Goodbye()
        {
            Console.WriteLine("Goodbye!");
        }

        private async Task Search()
        {
            Console.WriteLine("!!! SEARCH !!!");
            Console.WriteLine("Enter FirstName:");
            var firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName))
                firstName = null;
            Console.WriteLine("Enter Status:");
            var statusText = Console.ReadLine();
            byte? status = null;
            if (!string.IsNullOrWhiteSpace(statusText))
                status = byte.Parse(statusText);
            var result = await _userService.UsersAsync(new UserFilter {FirstName = firstName, Status = status});
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Number of users: {result.TotalUserCount}, number of pets: {result.TotalPetCount}");
            Console.ResetColor();
            foreach (var user in result.Users)
            {
                Console.WriteLine(
                    $"{user.FirstName} {user.LastName} - ExperiencePoints: {user.ExperiencePoints}, PetsDeleted: {user.PetsDeleted}, Status: {user.Status}, RowVersion: {BitConverter.ToString(user.RowVersion)}");
                foreach (var pet in user.Pets)
                {
                    Console.WriteLine($"    {pet.Name}");
                }
            }

            PressAnyKey();
        }

        private async Task Update()
        {
            Console.WriteLine("!!! UPDATE !!!");
            Console.WriteLine("Enter iterations count:");
            var iterations = int.Parse(Console.ReadLine());
            var result = _userUpdater.UpdateAsync(iterations);
            await foreach (var item in result)
            {
                Console.WriteLine(
                    $"ExperiencePoints: {item.ExperiencePoints} RowVersion: {BitConverter.ToString(item.RowVersion)}");
            }

            PressAnyKey();
        }

        private async Task Create()
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
            var result = await _userCreator.CreateAsync(user);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"UserId: {result.UserId} RowVersion: {BitConverter.ToString(result.RowVersion)}");
            Console.ResetColor();
            PressAnyKey();
        }
    }
}