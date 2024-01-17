Imports System.Text

Module Program
    Sub Main(args As String())
        Console.OutputEncoding = Encoding.UTF8
        Console.WriteLine("
 _       __     __                             __           __  ____               _____                                      ________    ____
| |     / /__  / /________  ____ ___  ___     / /_____     /  |/  (_)___  ___     / ___/      _____  ___  ____  ___  _____   / ____/ /   /  _/
| | /| / / _ \/ / ___/ __ \/ __ `__ \/ _ \   / __/ __ \   / /|_/ / / __ \/ _ \    \__ \ | /| / / _ \/ _ \/ __ \/ _ \/ ___/  / /   / /    / /  
| |/ |/ /  __/ / /__/ /_/ / / / / / /  __/  / /_/ /_/ /  / /  / / / / / /  __/   ___/ / |/ |/ /  __/  __/ /_/ /  __/ /     / /___/ /____/ /   
|__/|__/\___/_/\___/\____/_/ /_/ /_/\___/   \__/\____/  /_/  /_/_/_/ /_/\___/   /____/|__/|__/\___/\___/ .___/\___/_/      \____/_____/___/   
                                                                                                      /_/                                     ")
        Console.Write("
   ___         ___  ___  __    _____             __  _ ___     
  / _ )__ __  / _ )/ _ \/ /_  / ___/______ ___ _/ /_(_) _ \___ 
 / _  / // / / _  / // / __/ / /__/ __/ -_) _ `/ __/ / // / _ \
/____/\_, / /____/\___/\__/  \___/_/  \__/\_,_/\__/_/\___/_//_/
     /___/                     
        ")
        Console.Write("
The Project's GitHub Repo: https://github.com/B0tCreati0n/Minesweeper-CLI/

Find me here:
GitHub: https://github.com/B0tCreati0n/
Stack Overflow: https://stackoverflow.com/users/15663690/b0t-creati0n
Discord: @b0tcreati0n
Session Privat Messenger: 05fd663352e7c37cc2f26785ce2f8313d7a4d0a5e99b3c7cf9c4441ece63a93051



")
        Dim mapSize As Integer

        Do
            Console.Write("What is your preferred map size? ")
            mapSize = Console.ReadLine()

            If mapSize >= 100 Then
                Console.WriteLine("The max map size is 99x99 or lower💣")
            ElseIf mapSize <= 1 Then
                Console.WriteLine("The minimum map size is 2x2")
            Else
                Console.Write("Is it correct that you want a map size of " & mapSize & "x" & mapSize & "? Y/n ")
                Dim confirm As String = Console.ReadLine()

                If confirm.ToUpper() = "Y" Then
                    Exit Do ' Exit the loop if the user confirms with "Y"
                Else
                    Console.WriteLine("Please enter the map size again.")
                End If
            End If
        Loop
        ' Call the function to generate bomb coordinates
        Dim bombCoordinates() As Tuple(Of Integer, Integer) = GenerateBombCoordinates(mapSize)

        ' Displaying the generated bomb coordinates
        For Each coordinate In bombCoordinates
            Console.WriteLine($"Bomb at coordinates: ({coordinate.Item1}, {coordinate.Item2})")
        Next
    End Sub

    Function GenerateBombCoordinates(ByVal mapSize As Integer) As Tuple(Of Integer, Integer)()
        ' Array to store bomb coordinates
        Dim bombCoordinates(mapSize - 1) As Tuple(Of Integer, Integer)
        Dim rand As New Random()

        For i As Integer = 0 To mapSize - 1
            ' Generate random coordinates within the map
            Dim x As Integer = rand.Next(1, mapSize + 1)
            Dim y As Integer = rand.Next(1, mapSize + 1)

            ' Check if the coordinates already exist in bombCoordinates
            ' If yes, generate new coordinates until unique ones are found
            While Array.Exists(bombCoordinates, Function(coord) coord IsNot Nothing AndAlso coord.Item1 = x AndAlso coord.Item2 = y)
                x = rand.Next(1, mapSize + 1)
                y = rand.Next(1, mapSize + 1)
            End While

            ' Add the unique coordinates to bombCoordinates
            bombCoordinates(i) = New Tuple(Of Integer, Integer)(x, y)
        Next

        Return bombCoordinates
    End Function
End Module
