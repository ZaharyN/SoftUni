using System.Threading.Channels;

namespace CustomLinkedList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomLinkedList linkedList = new CustomLinkedList();

            linkedList.AddLast(10);
            linkedList.AddLast(20);
            linkedList.AddLast(30);
            linkedList.AddLast(40);
            linkedList.AddLast(50);
            linkedList.AddLast(60);
            linkedList.AddLast(70);
            linkedList.AddLast(80);

            int[] array = linkedList.ToArray();

            Console.WriteLine(string.Join(", ", array));
        }
    }
}
