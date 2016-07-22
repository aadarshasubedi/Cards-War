'This class is the functionality of the UI
' Must be the first class in the file
Public Class Form1
    Const NUMBER_OF_CARDS = 52
    Dim c As Card
    Dim deck(NUMBER_OF_CARDS) As Card
    'what happens when the form load, initialize the deck
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        c = New Card(suits.clubs, faceCardRank.King)
    End Sub

    'What happens when the flip card button is clicked
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Console.Write(c.getSuit)
        'Console.Write(c.getRank)
        getDeck()
    End Sub

    'subproceedure to fill the deck with 52 cards
    Private Sub getDeck()

        'Convery enum types to arrays of integers to use in the for loops
        Dim suits As Array
        suits = System.Enum.GetValues(GetType(suits))
        Dim faceCardRanks As Array
        faceCardRanks = System.Enum.GetValues(GetType(faceCardRank))

        'This will keep track of how many cards are currently in the array
        Dim numberOfCards As Integer
        numberOfCards = 0

        'for every suit we will fill in the ranks
        For Each suit As Integer In suits
            Console.Write(suit)
            'For the cards 2 to 9 without a face
            For rank As Integer = 2 To 9
                Console.Write(rank)
            Next
            'For the face cards
            For Each faceCardRank As Integer In faceCardRanks
                Console.Write(faceCardRank)
            Next
        Next

    End Sub
End Class

'Collection of the suits
'each enum type is represented by an integer
'e.g. spades = 2
Public Enum suits
    hearts
    diamonds
    spades
    clubs
End Enum

'Collection of the face cards
'each enum type is represented by an integer
Public Enum faceCardRank
    Jack = 10
    Queen = 11
    King = 12
    Ace = 13
End Enum

'This class represents a single card
'The constructor takes in a suit (see enum type) as an integer, and a rank as an integer (enum for face cards)
'The class can return the rank and suit
Public Class Card
    Private suit As Integer
    Private rank As Integer

    Public Sub New(ByVal suit As String, ByVal rank As Integer)
        MyClass.suit = suit
        MyClass.rank = rank
    End Sub

    Public Function getRank()
        Return MyClass.rank
    End Function

    Public Function getSuit()
        Return MyClass.suit
    End Function
End Class