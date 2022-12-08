using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Problems._2022;
internal class Day08
{
    public static string Part1(int[][] digits)
    {
        int notVisibleCount = 0;
        for (int y = 0; y < digits.Length; y++)
        {
            for (int x = 1; x < digits[y].Length - 1; x++)
            {
                int height = digits[y][x];
                for (int d = 0; d < x; d++)
                {
                    if (digits[y][d] >= height)
                    {
                        for (d = x + 1; d < digits[y].Length; d++)
                        {
                            if (digits[y][d] >= height)
                            {
                                for (d = 0; d < y; d++)
                                {
                                    if (digits[d][x] >= height)
                                    {
                                        for (d = y + 1; d < digits.Length; d++)
                                        {
                                            if (digits[d][x] >= height)
                                            {
                                                notVisibleCount++;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }
        return (digits.Length * digits.Length - notVisibleCount).ToString();
    }
    public static string Part2(int[][] digits)
    {
        int bestScore = int.MinValue;
        for (int y = 0; y < digits.Length; y++)
        {
            for (int x = 1; x < digits[y].Length - 1; x++)
            {
                int height = digits[y][x];
                int score = 1;
                int directionScore = 0;
                for (int d = x - 1; d >= 0; d--)
                {
                    directionScore++;
                    if (digits[y][d] >= height)
                    {
                        break;
                    }
                }
                score *= directionScore;
                directionScore = 0;
                for (int d = x + 1; d < digits[y].Length; d++)
                {
                    directionScore++;
                    if (digits[y][d] >= height)
                    {
                        break;
                    }
                }
                score *= directionScore;
                directionScore = 0;
                for (int d = y - 1; d >= 0; d--)
                {
                    directionScore++;
                    if (digits[d][x] >= height)
                    {
                        break;
                    }
                }
                score *= directionScore;
                directionScore = 0;
                for (int d = y + 1; d < digits.Length; d++)
                {
                    directionScore++;
                    if (digits[d][x] >= height)
                    {
                        break;
                    }
                }
                score *= directionScore;
                bestScore = int.Max(bestScore, score);
            }
        }
        return bestScore.ToString();
    }
}
