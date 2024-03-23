Overview:

This project involves implementing a music playlist structure in C#, capable of adding, removing, and iterating through songs/tracks. The project is divided into three main parts:

Implementing a Music Track
Implementing a Doubly Linked List
Implementing a Playlist
Each part contributes to the functionality of the overall Spotify playlist implementation.

Part 1: Implementing a Music Track

The MusicTrack class is designed to hold information about a single music track, including its name, artist name, album name, and duration in seconds.
Ensure that all string entries are non-null and non-empty, and the duration of the song is at least 1 second long.
A constructor is provided that initializes the track with the given information, throwing exceptions for invalid arguments.
Part 2: Implementing a Doubly Linked List

The DoublyLinkedList class is implemented to store a collection of tracks using a doubly linked list structure.
It includes functions to add tracks, remove tracks, get the size of the playlist, and iterate through the playlist.
The list maintains head and tail pointers along with a size variable to keep track of the list's state.
Part 3: Implementing a Playlist

The Playlist class represents a playlist, containing a name, count of songs, and a doubly linked list of tracks.
Tracks can be added to the playlist using the Add() method and removed using the Delete() method.
Additionally, methods Next() and Previous() are provided to move forward or backward through the playlist.
The ToString() method is overridden to display details of the current track.
Instructions for Running the Code:

Clone the repository containing the project files.
Open the project in a C# compatible IDE such as Visual Studio.
Compile and run the project.
Test the functionality of the implemented classes using appropriate test cases.
Additional Notes:

Ensure that the provided constructors and methods are used correctly, and handle any exceptions as specified in the requirements.
Make sure to follow the guidelines for adding, removing, and iterating through tracks to maintain the integrity of the playlist structure.
