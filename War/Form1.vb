'This class is the functionality of the UI
' Must be the first class in the file
Public Class Form1
    Dim cardPile As New List(Of Card)
    Dim player As Player
    Dim computer As Player

    'what happens when the form load, initialize the deck
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim deck As Deck = New Deck()
        player = New Player(deck, typesOfPlayer.human)
        computer = New Player(deck, typesOfPlayer.computer)
        displayNumberOfCards()
    End Sub

    'the subproceedure flipCards is called in the event that the Button_1 "Flip Cards" is clicked
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        flipCards()
    End Sub

    'This subprodceedure flips a player card and a computer card
    'Cards are added to the card pile until either player of computer has a better card
    Private Sub flipCards()
        Dim results As String
        Dim playerCard As Card = player.getCard()
        Dim computerCard As Card = computer.getCard()
        cardPile.Add(playerCard)
        cardPile.Add(computerCard)
        'If the cards are the same rank then we have war
        'while we have war you need to draw more cards
        While (playerCard.getRank = computerCard.getRank)
            'In war you both draw 3 cards "face down" and then flip the 4th card up
            'To achieve the same result in our code we add 4 cards to each pile but 
            'will only compare the last card of each (replace the playerCard and compuerCard varaibles)
            GameResultsText.Text &= "WAR!!!" & vbCrLf
            For i As Integer = 1 To 4
                playerCard = player.getCard()
                computerCard = computer.getCard()
                cardPile.Add(playerCard)
                cardPile.Add(computerCard)
            Next
        End While
        'This is a string that we print out to the Game Results Text Box
        results = "You: " & playerCard.printCard() & " | Computer: " & computerCard.printCard() & vbCrLf
        '&= allows us to keep adding to the textbox.
        GameResultsText.Text &= results
        'Check who won the card flip
        'When the player wins we add the cards from the pile to their hand
        'When the computer wins we add the cards from the pile to their hand
        If (playerCard.getRank > computerCard.getRank) Then
            player.addCards(cardPile)
            GameResultsText.Text &= "You Won +" & cardPile.Count / 2 & vbCrLf
        Else
            computer.addCards(cardPile)
            GameResultsText.Text &= "Computer Won +" & cardPile.Count / 2 & vbCrLf
        End If
        'Show number of cards in each players hands
        displayNumberOfCards()
        'Clear the cards added to the pile.
        cardPile.Clear()
    End Sub

    'Displays each of the players number of cards in their respective textboxes
    Private Sub displayNumberOfCards()
        humanNumber.Text = player.getNumberOfCards()
        computerNumber.Text = computer.getNumberOfCards()
    End Sub
End Class

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

    Public Function printCard()
        Dim suitName As String
        Dim currentSuit As Integer
        suitName = "none"
        currentSuit = getSuit()

        Select Case currentSuit
            Case 0
                suitName = "hearts"
            Case 1
                suitName = "diamonds"
            Case 2
                suitName = "spades"
            Case 3
                suitName = "clubs"
        End Select
        Return suitName & ", " & getRank()
    End Function
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

    Private Sub shuffleDeck(ByRef items() As Card)
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

Public Class Player
    Private cardHand As New List(Of Card)
    Private discardedHand As New List(Of Card)
    Public typeOfPlayer

    Public Sub New(ByRef deck As Deck, ByVal typeOfPlayer As Boolean)
        MyClass.typeOfPlayer = typeOfPlayer
        getInitialHand(deck)
    End Sub

    Private Sub getInitialHand(ByRef deck As Deck)
        If (MyClass.typeOfPlayer = typesOfPlayer.human) Then
            For i As Integer = 0 To 25
                cardHand.Add(deck.getCard(i))
                cardHand(i).printCard()
            Next
        Else
            For i As Integer = 26 To 51
                cardHand.Add(deck.getCard(i))
                cardHand(i - 26).printCard()
            Next
        End If
    End Sub

    Public Function getCard()
        Dim card As Card
        card = cardHand(0)
        cardHand.RemoveAt(0)
        Return card
    End Function

    Public Sub addCards(ByRef cards As List(Of Card))
        For Each card As Card In cards
            cardHand.Add(card)
        Next
    End Sub

    Public Sub reshuffleCards()

    End Sub

    Public Function getNumberOfCards()
        Return MyClass.cardHand.LongCount
    End Function

End Class

'Collection of the suits
'each enum type is represented by an integer
'e.g. spades = 2
Public Enum suits
    hearts = 0
    diamonds = 1
    spades = 2
    clubs = 3
End Enum

'Collection of the face cards
'each enum type is represented by an integer
Public Enum faceCardRank
    Jack = 11
    Queen = 12
    King = 13
    Ace = 14
End Enum

Public Enum typesOfPlayer
    human = True
    computer = False
End Enum
'TODO: second pile for each player that gets reshuffled in everytime their cardhand runs out