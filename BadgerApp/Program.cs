using Badger;

namespace BadgerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 10,7,4,
            // 170,1,1,
            // 139,19,6,
            // 29,16,8,

            //s291608
            // s1391906
            var badge = GetFromServer.ParseBadgeData("b10074" +
                "s170011" +
                "s139196" +
                "s29168"); // s29014"); // 10,3,4,29,1,4,


            //"b0502Xs13181s01014");// s05010s05011s05012s05013s05014s05015s05016s05017s05018");

            badge.Render();

            // Console.WriteLine("Hello, World!");
        }
    }
}