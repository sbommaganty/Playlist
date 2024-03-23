using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part01
{
    internal class Program
    {
        public class Track
        {
            private string name;
            private string artistName;
            private string albumName;
            private int duration; //duration in seconds

            public Track(string name, string artistName, string albumName, int duration)
            {
                SetName(name);
                SetArtistName(artistName);
                SetAlbumName(albumName);
                SetDuration(duration);
            }

            /* setters below throw exceptions for invalid input */
            // Setter method for name
            public void SetName(string name)
            {
                // Check for null or empty input
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("The Name cannot be null or empty");
                this.name = name;
            }

            // Setter method for artist name
            public void SetArtistName(string artistName)
            {
                // Check for null or empty input
                if (string.IsNullOrEmpty(artistName))
                    throw new ArgumentException("The Artist name cannot be null or empty");
                this.artistName = artistName;
            }

            // Setter method for album name
            public void SetAlbumName(string albumName)
            {
                // Check for null or empty input
                if (string.IsNullOrEmpty(albumName))
                    throw new ArgumentException("The Album name cannot be null or empty");
                this.albumName = albumName;
            }

            // Setter method for duration
            public void SetDuration(int duration)
            {
                // Check for invalid duration (less than 1 second)
                if (duration < 1)
                    throw new ArgumentException("The Duration must be at least 1 second");
                this.duration = duration;
            }

            // Getter method for name
            public string GetName()
            {
                return name;
            }

            // Getter method for artist name
            public string GetArtistName()
            {
                return artistName;
            }

            // Getter method for album name
            public string GetAlbumName()
            {
                return albumName;
            }

            // Getter method for duration
            public int GetDuration()
            {
                return duration;
            }
        }

        //DNode represents a node for DoublyLinkedList 

        public class DNode
        {
            protected Track song; //each node holds a song
            protected DNode next, prev; //pointers to next and prev nodes

            //Constructor that creates a node
            public DNode(Track t, DNode p, DNode n)
            {
                song = t;
                next = n;
                prev = p;
            }

            public Track GetTrack() { return song; }
            public DNode GetPrev() { return prev; }
            public DNode GetNext() { return next; }
            public void SetTrack(Track t) { song = t; }
            public void SetPrev(DNode p) { prev = p; }
            public void SetNext(DNode n) { next = n; }

        }

        public class DoublyLinkedList
        {
            protected int size;
            protected DNode header, tail;

            public DoublyLinkedList()
            {
                size = 0; //initial size of list 
                header = new DNode(null, null, null); //header points to null
                tail = new DNode(null, header, null); //tail's prev node is header
                header.SetNext(tail); //header's next points to to tail
            }

            public int Size()
            {
                return size;
            } //return size of list
            public bool IsEmpty()
            {
                return size == 0;
            } //return true if list is empty, false otherwise

            public DNode GetFirst()
            {
                if (IsEmpty())
                    throw new InvalidOperationException("The List is empty");
                return header.GetNext();
            } //return first node, or throws InvlaidOperationException if list is empty
            public DNode GetLast()
            {
                if (IsEmpty())
                    throw new InvalidOperationException("The List is empty");
                return tail.GetPrev();
            } //return last node, or throws InvlaidOperationException

            /* Return the node before node v or throws ArgumentException */
            public DNode GetPrev(DNode v)
            {
                if (v == header)
                    throw new ArgumentException("We Cannot get previous of header node");
                return v.GetPrev();
            }
            /* Return the node before node v throws ArgumentException if current node is tail. */
            public DNode GetNext(DNode v)
            {
                if (v == tail)
                    throw new ArgumentException("We Cannot get next of tail node");
                return v.GetNext();
            }
            /*Insert the node z before node e or  throws ArgumentException if current node is header. */
            public void AddBefore(DNode e, DNode z)
            {
                DNode u = GetPrev(e);
                z.SetPrev(u);
                z.SetNext(e);
                e.SetPrev(z);
                u.SetNext(z);
                size++;
            }
            /*Insert the node z after node e. Throw ArgumentException if current node is tail. */
            public void AddAfter(DNode e, DNode z)
            {
                DNode w = GetNext(e);
                z.SetPrev(e);
                z.SetNext(w);
                w.SetPrev(z);
                e.SetNext(z);
                size++;
            }
            /* Add node v at start of list */
            public void AddFirst(DNode v)
            {
                AddAfter(header, v);
            }
            /* Add node v at end of list */
            public void AddLast(DNode v)
            {
                AddBefore(tail, v);
            }
            /* Removes node v from list */
            public void Remove(DNode e)
            {
                DNode u = GetPrev(e);
                DNode w = GetNext(e);
                w.SetPrev(u);
                u.SetNext(w);
                e.SetPrev(null);
                e.SetNext(null);
                size--;
            }
            /* returns true if node has a previous node, false otherwise */
            public bool HasPrev(DNode s)
            {
                return s != header;
            }
            /* returns true if node has next node, false otherwise */
            public bool HasNext(DNode y)
            {
                return y != tail;
            }
        }

        public class Playlist
        {
            private string name;
            private int count;
            private DoublyLinkedList tracks;
            private DNode curr; //current playlist position

            public Playlist(string name)
            {
                this.name = name;
                count = 0;
                tracks = new DoublyLinkedList();
                curr = null; // Initially, no track is selected
            }

            public string GetName()
            {
                return name;
            } //return playlist name

            public int GetCount()
            {
                return count;
            } //return number of songs in album

            public void SetName(string name)
            {
                this.name = name;
            } //set name of playlist

            public void Add(Track t)
            {
                tracks.AddLast(new DNode(t, null, null));
                count++;
            } //add song to start or end of playlist (you decide). Increase size by 1


            public void Remove(Track t)
            {
                DNode node = FindNode(t);
                if (node != null)
                {
                    tracks.Remove(node);
                    count--;
                    // If the removed track was the current track, set the current track to null
                    if (curr == node)
                        curr = null;
                }
            }

            // Method to move to the next track
            public void Next()
            {
                if (curr != null && tracks.HasNext(curr))
                {
                    curr = tracks.GetNext(curr);
                }
            }

            // Method to move to the previous track
            public void Previous()
            {
                if (curr != null && tracks.HasPrev(curr))
                {
                    curr = tracks.GetPrev(curr);
                }
            }

            // Method to display the current track
            public string GetCurrentTrack()
            {
                if (curr != null)
                {
                    Track currentTrack = curr.GetTrack();
                    return $"The Current Track is: {currentTrack.GetName()} by {currentTrack.GetArtistName()}";
                }
                else
                {
                    return "No track is ever selected";
                }
            }

            public void Play()
            {
                if (tracks.IsEmpty())
                {
                    Console.WriteLine("There are no tracks in the playlist or is empty.");
                    return;
                }

                curr = tracks.GetFirst();
            }

            // Method to find a track by name
            public Track GetTrackByName(string trackName)
            {
                DNode current = tracks.GetFirst();
                while (current != null)
                {
                    if (current.GetTrack().GetName() == trackName)
                    {
                        return current.GetTrack();
                    }
                    current = tracks.GetNext(current);
                }
                return null;
            }


            // Method to find a node containing a specific track
            private DNode FindNode(Track t)
            {
                DNode current = tracks.GetFirst();
                while (current != null)
                {
                    if (current.GetTrack() == t)
                    {
                        return current;
                    }
                    current = tracks.GetNext(current);
                }
                return null;
            }
        }

 


        static void Main(string[] args)
        {
            // Create a new playlist
            Playlist myPlaylist = new Playlist("My music Playlist");

            // Loop to interact with the playlist
            while (true)
            {
                Console.WriteLine("\nWELCOME TO MUSIC PLAYER\nMenu:");
                Console.WriteLine("1. Add a track in the playlist");
                Console.WriteLine("2. Play the playlist from the start");
                Console.WriteLine("3. Play the next track");
                Console.WriteLine("4. Play the previous track");
                Console.WriteLine("5. Remove a track from the playlist");
                Console.WriteLine("6. Display current track playing");
                Console.WriteLine("7. Display playlist information");
                Console.WriteLine("8. Exit the music player");
                Console.Write("Enter your choice (numbers only): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter track name: ");
                        string trackName = Console.ReadLine();
                        Console.Write("Enter artist name: ");
                        string artistName = Console.ReadLine();
                        Console.Write("Enter album name: ");
                        string albumName = Console.ReadLine();
                        Console.Write("Enter duration (in seconds): ");
                        int duration = int.Parse(Console.ReadLine());
                        Track newTrack = new Track(trackName, artistName, albumName, duration);
                        myPlaylist.Add(newTrack);
                        Console.WriteLine("Track added successfully in the playlist!");
                        break;
                    case "2":
                        myPlaylist.Play();
                        Console.WriteLine("Playing the first track in the playlist...");
                        break;
                    case "3":
                        myPlaylist.Next();
                        Console.WriteLine("Moving to the next track in the playlist...");
                        break;
                    case "4":
                        myPlaylist.Previous();
                        Console.WriteLine("Moving to the previous track in the playlist...");
                        break;
                    case "5":
                        Console.Write("Enter track name to remove from the playlist: ");
                        string trackToRemove = Console.ReadLine();
                        Track track = myPlaylist.GetTrackByName(trackToRemove);
                        if (track != null)
                        {
                            myPlaylist.Remove(track);
                            Console.WriteLine("Track removed successfully from the playlist!");
                        }
                        else
                        {
                            Console.WriteLine("Track not found in the playlist.");
                        }
                        break;
                    case "6":
                        Console.WriteLine("Current track playing is:");
                        Console.WriteLine(myPlaylist.GetCurrentTrack());
                        break;
                    case "7":
                        Console.WriteLine($"Playlist: {myPlaylist.GetName()}, Number of tracks: {myPlaylist.GetCount()}");
                        break;
                    case "8":
                        Console.WriteLine("The  program is excited...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
