'This class is the functionality of the UI
' Must be the first class in the file
Public Class Form1
    Public cardPile As New List(Of Card)
    Public player As HumanPlayer
    'what happens when the form load, initialize the deck
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim deck As Deck = New Deck()
        player = New HumanPlayer(deck)
    End Sub

    'What happens when the flip card button is clicked
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Console.Write(c.getSuit)
        'Console.Write(c.getRank)
        'getDeck()
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
    Jack = 11
    Queen = 12
    King = 13
    Ace = 14
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

    Public Sub printCard()
        Console.Write(getSuit() & ", " & getRank() & " | ")
    End Sub
End Class

Public Class Deck
    Const NUMBER_OF_CARDS = 51
    Private deck(NUMBER_OF_CARDS) As Card

    Public Sub New()
        generateCards()
    End Sub

    'subproceedure to fill the deck with 52 cards
    Public Function getDeck()
        Return deck
    End Function

    Public Function getCard(ByVal index)
        Return deck(index)
    End Function

    Private Sub generateCards()
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
            'Console.Write(suit)
            'For the cards 2 to 9 without a face
            For rank As Integer = 2 To 10
                'Create a new card and add it to the deck array
                Dim c As Card = New Card(suit, rank)
                deck(numberOfCards) = c
                numberOfCards = numberOfCards + 1
            Next
            'For the face cards
            For Each faceCardRank As Integer In faceCardRanks
                Dim c As Card = New Card(suit, faceCardRank)
                deck(numberOfCards) = c
                numberOfCards = numberOfCards + 1
            Next
        Next

        'To truly randomize a deck of cards you need to shuffle 7 times
        For i As Integer = 1 To 7
            shuffleDeck(deck)
        Next
    End Sub

    Private Sub shuffleDeck(ByVal items() As Card)
        Dim max_index As Integer = items.Length - 1
        Dim rnd As New Random
        For i As Integer = 0 To max_index - 1
            ' Pick an item for position i.
            Dim j As Integer = rnd.Next(i, max_index + 1)

            ' Swap them.
            Dim temp As Card = items(i)
            items(i) = items(j)
            items(j) = temp
        Next i
    End Sub
End Class

Public Class HumanPlayer
    Private cardHand As New List(Of Card)

    Public Sub New(ByRef deck As Deck)
        getInitialHand(deck)
    End Sub

    Private Sub getInitialHand(ByRef deck As Deck)
        For i As Integer = 0 To 25
            cardHand.Add(deck.getCard(i))
            cardHand(i).printCard()
        Next
    End Sub

    Public Function getCard()
        Dim card As Card
        card = cardHand(0)
        cardHand.RemoveAt(0)
        Return card
    End Function

End Class

Public Class ComputerPlayer

End Class