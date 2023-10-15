string input = Console.ReadLine();

Stack<char> parenthesis = new Stack<char>();

foreach (var item in input)
{
    if (item == '{' || item == '[' || item == '(')
    {
        parenthesis.Push(item);
    }
    else if (item == ')')
    {
        if (parenthesis.Count > 0 && parenthesis.Peek() == '(')
        {
            parenthesis.Pop();
        }
        else
        {
            Console.WriteLine("NO");
            return;
        }
    }
    else if (item == '}')
    {
        if (parenthesis.Count > 0 && parenthesis.Peek() == '{')
        {
            parenthesis.Pop();
        }
        else
        {
            Console.WriteLine("NO");
            return;
        }
    }
    else if (item == ']')
    {
        if (parenthesis.Count > 0 && parenthesis.Peek() == '[')
        {
            parenthesis.Pop();
        }
        else
        {
            Console.WriteLine("NO");
            return;
        }
    }
}
Console.WriteLine("YES");