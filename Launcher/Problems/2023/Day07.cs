using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Problems._2023;
internal class Day07
{
    static int HandRank(IEnumerable<char> cards)
    {
        if (cards.Distinct().Count() == 1) return 0;
        if (cards.Distinct().Count() == 2 && cards.Count(card => card == cards.First()) is int fourCount && (fourCount == 1 || fourCount == 4)) return 1;
        if (cards.Distinct().Count() == 2 && cards.Count(card => card == cards.First()) is int fullHouseCount && (fullHouseCount == 2 || fullHouseCount == 3)) return 2;
        if (cards.Distinct().Count() == 3 && cards.Max(card => cards.Count(c => card == c)) == 3) return 3;
        if (cards.Distinct().Count() == 3) return 4;
        if (cards.Distinct().Count() == 4) return 5;
        return 6;
    }

    const string exampleInput = "32T3K 765\r\nT55J5 684\r\nKK677 28\r\nKTJJT 220\r\nQQQJA 483";
    [Example(exampleInput, "6440")]
    public static string Part1([RemoveEmpty] string[] lines)
    {
        const string Cards = "AKQJT98765432";
        static int CardRank(char card) => Cards.IndexOf(card);

        var bets = new List<(string hand, long bet)>();
        foreach (var line in lines)
        {
            bets.Add((line.Before(" "), line.After(" ").ParseInt()));
        }
        bets = bets
            .OrderBy(bet => HandRank(bet.hand))
            .ThenBy(bet => CardRank(bet.hand[0]))
            .ThenBy(bet => CardRank(bet.hand[1]))
            .ThenBy(bet => CardRank(bet.hand[2]))
            .ThenBy(bet => CardRank(bet.hand[3]))
            .ThenBy(bet => CardRank(bet.hand[4]))
            .ToList();
        bets.Reverse();
        var sum = bets.Select((bet, i) => bet.bet * (i + 1)).Sum();
        return sum.ToString();
    }

    [Example(exampleInput, "5905")]
    public static string Part2([RemoveEmpty] string[] lines)
    {
        const string Cards = "AKQT98765432J";
        static int CardRank(char card) => Cards.IndexOf(card);
        static IEnumerable<string> EnumerateJokerHands(IEnumerable<char> cards)
        {
            if(cards.Contains('J'))
            {
                var s = new string(cards.ToArray());
                var i = s.IndexOf('J');
                foreach (var card in Cards.SkipLast(1))
                {
                    var cardsArray = cards.ToArray();
                    cardsArray[i] = card;
                    s = new string(cardsArray);
                    foreach (var item in EnumerateJokerHands(s))
                    {
                        yield return item;
                    }
                }
            }
            else
            {
                yield return new string(cards.ToArray());
            }
        }
        static int MinHandRank(IEnumerable<char> cards)
        {
            return HandRank(EnumerateJokerHands(cards).MinBy(HandRank)!);
        }

        var bets = new List<(string hand, long bet)>();
        foreach (var line in lines)
        {
            bets.Add((line.Before(" "), line.After(" ").ParseInt()));
        }
        bets = bets
            .OrderBy(bet => MinHandRank(bet.hand))
            .ThenBy(bet => CardRank(bet.hand[0]))
            .ThenBy(bet => CardRank(bet.hand[1]))
            .ThenBy(bet => CardRank(bet.hand[2]))
            .ThenBy(bet => CardRank(bet.hand[3]))
            .ThenBy(bet => CardRank(bet.hand[4]))
            .ToList();
        bets.Reverse();
        var sum = bets.Select((bet, i) => bet.bet * (i + 1)).Sum();
        return sum.ToString();
    }
}
