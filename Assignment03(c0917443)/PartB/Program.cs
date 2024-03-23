using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartB
{
    // Represents a node in the doubly linked list
    public class DNode
    {
        protected char Bracket;
        protected DNode Next, Prev;

        // Initializes a node with given bracket, previous, and next nodes
        public DNode(char bracket, DNode prev, DNode next)
        {
            Bracket = bracket;
            Prev = prev;
            Next = next;
        }

        // Returns the bracket stored in this node
        public char GetBracket() { return Bracket; }

        // Returns the previous node of this node
        public DNode GetPrev() { return Prev; }

        // Returns the next node of this node
        public DNode GetNext() { return Next; }

        // Sets the bracket stored in this node
        public void SetBracket(char bracket) { Bracket = bracket; }

        // Sets the previous node of this node
        public void SetPrev(DNode prev) { Prev = prev; }

        // Sets the next node of this node
        public void SetNext(DNode next) { Next = next; }
    }

    // Represents a doubly linked list
    public class DoublyLinkedList
    {
        protected int Size;
        protected DNode Header, Tail;

        // Initializes an empty doubly linked list
        public DoublyLinkedList()
        {
            Size = 0;
            Header = new DNode('\0', null, null);
            Tail = new DNode('\0', Header, null);
            Header.SetNext(Tail);
        }

        // Returns the size of the doubly linked list
        public int GetSize() { return Size; }

        // Checks if the doubly linked list is empty
        public bool IsEmpty() { return Size == 0; }

        // Returns the first node in the doubly linked list
        public DNode GetFirst()
        {
            if (IsEmpty())
                throw new InvalidOperationException("List is empty");
            return Header.GetNext();
        }

        // Returns the last node in the doubly linked list
        public DNode GetLast()
        {
            if (IsEmpty())
                throw new InvalidOperationException("List is empty");
            return Tail.GetPrev();
        }

        // Returns the previous node of a given node
        public DNode GetPrev(DNode node)
        {
            if (node == Header)
                throw new ArgumentException("Cannot get previous of header");
            return node.GetPrev();
        }

        // Returns the next node of a given node
        public DNode GetNext(DNode node)
        {
            if (node == Tail)
                throw new ArgumentException("Cannot get next of tail");
            return node.GetNext();
        }

        // Adds a new node before a given node
        public void AddBefore(DNode node, DNode newNode)
        {
            DNode prevNode = GetPrev(node);
            newNode.SetPrev(prevNode);
            newNode.SetNext(node);
            node.SetPrev(newNode);
            prevNode.SetNext(newNode);
            Size++;
        }

        // Adds a new node after a given node
        public void AddAfter(DNode node, DNode newNode)
        {
            DNode nextNode = GetNext(node);
            newNode.SetPrev(node);
            newNode.SetNext(nextNode);
            nextNode.SetPrev(newNode);
            node.SetNext(newNode);
            Size++;
        }

        // Adds a new node as the first node in the doubly linked list
        public void AddFirst(DNode newNode)
        {
            AddAfter(Header, newNode);
        }

        // Adds a new node as the last node in the doubly linked list
        public void AddLast(DNode newNode)
        {
            AddBefore(Tail, newNode);
        }

        // Removes a given node from the doubly linked list
        public void Remove(DNode node)
        {
            DNode prevNode = GetPrev(node);
            DNode nextNode = GetNext(node);
            nextNode.SetPrev(prevNode);
            prevNode.SetNext(nextNode);
            node.SetPrev(null);
            node.SetNext(null);
            Size--;
        }

        // Checks if a given node has a previous node
        public bool HasPrev(DNode node)
        {
            return node != Header;
        }

        // Checks if a given node has a next node
        public bool HasNext(DNode node)
        {
            return node != Tail;
        }
    }

    // Represents a stack using a doubly linked list
    public class Stack
    {
        private DoublyLinkedList stack;

        // Initializes an empty stack
        public Stack()
        {
            stack = new DoublyLinkedList();
        }

        // Returns the size of the stack
        public int GetSize()
        {
            return stack.GetSize();
        }

        // Pushes a new element onto the stack
        public void Push(char bracket)
        {
            stack.AddLast(new DNode(bracket, null, null));
        }

        // Pops the top element from the stack
        public char Pop()
        {
            if (stack.IsEmpty())
                throw new InvalidOperationException("The Stack is empty");

            DNode node = stack.GetLast();
            stack.Remove(node);
            return node.GetBracket();
        }

        // Returns the top element of the stack without removing it
        public char Top()
        {
            if (stack.IsEmpty())
                throw new InvalidOperationException(" The Stack is empty");

            DNode node = stack.GetLast();
            return node.GetBracket();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stack bracketStack = new Stack();
            Console.WriteLine("Enter the input pattern:");
            string userInput = Console.ReadLine();

            foreach (char bracket in userInput)
            {
                if (bracket == '(' || bracket == '[' || bracket == '{')
                    bracketStack.Push(bracket);
                else if (bracket == ')' || bracket == ']' || bracket == '}')
                {
                    if (bracketStack.GetSize() == 0)
                    {
                        Console.WriteLine("Match not found");
                        Console.ReadLine();
                        return;
                    }

                    char topBracket = bracketStack.Top();
                    if ((bracket == ')' && topBracket == '(') || (bracket == ']' && topBracket == '[') || (bracket == '}' && topBracket == '{'))
                        bracketStack.Pop();
                    else
                    {
                        Console.WriteLine("Match not found");
                        Console.ReadLine();
                        return;
                    }
                }
            }

            if (bracketStack.GetSize() == 0)
                Console.WriteLine("Match found");
            else
                Console.WriteLine("Match not found");

            Console.ReadLine();
        }
    }
}
