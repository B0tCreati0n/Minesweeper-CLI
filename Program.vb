Imports System.Text

Module Program
    Sub Main(args As String())
        Console.OutputEncoding = Encoding.UTF8

        CConsole.WriteLine("
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
        Console.Write("Before we start, please set your cmd font to ")
        Dim mapSize As Integer

        Do
            Console.Write("What is your preferred map size? 💣⚑")
            mapSize = Console.ReadLine()

            If mapSize >= 100 Then
                Console.WriteLine("The max map size is 99x99 or lower")
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

        ' Main game loop
        Do
            Console.Write("Enter command (e.g., 'check 1,2', 'setflag 3,4', 'removeFlag 5,6'): ")
            Dim command As String = Console.ReadLine()

            If command.ToLower().StartsWith("check") Then
                Dim coordinates() As Integer = ParseCoordinates(command)
                If coordinates IsNot Nothing Then
                    CheckCoordinates(coordinates, bombCoordinates, mapSize)
                Else
                    Console.WriteLine("Invalid command. Please use the format 'check coordinate1,coordinate2'.")
                End If
            ElseIf command.ToLower().StartsWith("setflag") Then
                Dim coordinates() As Integer = ParseCoordinates(command)
                If coordinates IsNot Nothing Then
                    FlagCoordinates(coordinates)
                Else
                    Console.WriteLine("Invalid command. Please use the format 'flag coordinate1,coordinate2'.")
                End If
            ElseIf command.ToLower().StartsWith("removeflag") Then
                Dim coordinates() As Integer = ParseCoordinates(command)
                If coordinates IsNot Nothing Then
                    RemoveFlagCoordinates(coordinates)
                Else
                    Console.WriteLine("Invalid command. Please use the format 'removeFlag coordinate1,coordinate2'.")
                End If
            Else
                Console.WriteLine("Invalid command. Please enter a valid command.")
            End If

        Loop While True
    End Sub

    Function ParseCoordinates(ByVal command As String) As Integer()
        Dim coordinates() As Integer = Nothing

        Try
            Dim coordinatesString As String = command.Substring(command.IndexOf(" ") + 1)
            Dim coordinatesArray() As String = coordinatesString.Split(","c)
            ReDim coordinates(coordinatesArray.Length - 1)

            For i As Integer = 0 To coordinatesArray.Length - 1
                coordinates(i) = Integer.Parse(coordinatesArray(i))
            Next

        Catch ex As Exception
            Return Nothing
        End Try

        Return coordinates
    End Function

    Sub CheckCoordinates(ByVal coordinates() As Integer, ByVal bombCoordinates() As Tuple(Of Integer, Integer), ByVal mapSize As Integer)
        ' Implementation for the "check" command
        Dim x As Integer = coordinates(0)
        Dim y As Integer = coordinates(1)

        If CoordinatesAreValid(x, y, mapSize) Then
            If IsBombAtCoordinates(x, y, bombCoordinates) Then
                Console.WriteLine("Boom! You hit a bomb. Game Over.")
                ShowAllBombCoordinates(bombCoordinates)
            Else
                Dim bombsAround As Integer = CountBombsAround(x, y, bombCoordinates)
                Console.WriteLine($"No bomb at ({x},{y}). {bombsAround} bomb(s) around.")
            End If
        Else
            Console.WriteLine("Invalid coordinates. Please enter valid coordinates.")
        End If
    End Sub

    Sub FlagCoordinates(ByVal coordinates() As Integer)
        ' Implementation for the "flag" command
        Dim x As Integer = coordinates(0)
        Dim y As Integer = coordinates(1)

        If CoordinatesAreValid(x, y, mapSize) Then
            ' Add your logic to flag the coordinates
            Console.WriteLine($"Flag set at coordinates: ({x},{y})")
        Else
            Console.WriteLine("Invalid coordinates. Please enter valid coordinates.")
        End If
    End Sub

    Sub RemoveFlagCoordinates(ByVal coordinates() As Integer)
        ' Implementation for the "removeFlag" command
        Dim x As Integer = coordinates(0)
        Dim y As Integer = coordinates(1)

        If CoordinatesAreValid(x, y, mapSize) Then
            ' Add your logic to remove the flag at the coordinates
            Console.WriteLine($"Flag removed from coordinates: ({x},{y})")
        Else
            Console.WriteLine("Invalid coordinates. Please enter valid coordinates.")
        End If
    End Sub

    ' Helper function to check if coordinates are within the valid range
    Function CoordinatesAreValid(ByVal x As Integer, ByVal y As Integer, ByVal mapSize As Integer) As Boolean
        Return x >= 1 AndAlso x <= mapSize AndAlso y >= 1 AndAlso y <= mapSize
    End Function

    ' Helper function to check if there is a bomb at the specified coordinates
    Function IsBombAtCoordinates(ByVal x As Integer, ByVal y As Integer, ByVal bombCoordinates() As Tuple(Of Integer, Integer)) As Boolean
        Return bombCoordinates.Any(Function(coord) coord IsNot Nothing AndAlso coord.Item1 = x AndAlso coord.Item2 = y)
    End Function

    ' Helper function to count the number of bombs around the specified coordinates
    Function CountBombsAround(ByVal x As Integer, ByVal y As Integer, ByVal bombCoordinates() As Tuple(Of Integer, Integer)) As Integer
        Dim bombCount As Integer = 0

        For Each coord In bombCoordinates
            If coord IsNot Nothing AndAlso Math.Abs(coord.Item1 - x) <= 1 AndAlso Math.Abs(coord.Item2 - y) <= 1 Then
                bombCount += 1
            End If
        Next

        Return bombCount
    End Function

    ' Helper function to display all bomb coordinates (for debugging)
    Sub ShowAllBombCoordinates(ByVal bombCoordinates() As Tuple(Of Integer, Integer))
        Console.WriteLine("Bomb coordinates:")
        For Each coord In bombCoordinates
            If coord IsNot Nothing Then
                Console.WriteLine($"({coord.Item1},{coord.Item2})")
            End If
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
