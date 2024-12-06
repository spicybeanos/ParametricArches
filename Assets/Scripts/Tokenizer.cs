using System.Collections.Generic;
using UnityEngine.UI;

public class Tokenizer
{
    public static List<Token> TokenizeString(string input)
    {
        List<Token> tokens = new List<Token>();
        for (int current = 0; current < input.Length; current++)
        {
            switch (input[current])
            {
                case '>':
                    tokens.Add(new Token(current, ">", TokenType.Greater)); break;
                case '<':
                    tokens.Add(new Token(current, "<", TokenType.Lesser)); break;
                case '-':
                    tokens.Add(new Token(current, "-", TokenType.Minus)); break;
                case '+':
                    tokens.Add(new Token(current, "+", TokenType.Plus)); break;
                case '*':
                    tokens.Add(new Token(current, "*", TokenType.Multiply)); break;
                case '/':
                    tokens.Add(new Token(current, "/", TokenType.Divide)); break;
                case '%':
                    tokens.Add(new Token(current, "%", TokenType.Modulus)); break;
                case '^':
                    tokens.Add(new Token(current, "^", TokenType.Power)); break;

                case '(':
                    tokens.Add(new Token(current, "(", TokenType.OpenParenth)); break;
                case ')':
                    tokens.Add(new Token(current, ")", TokenType.CloseParenth)); break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    {
                        int start = current, length = 0;
                        for (
                            ; current < input.Length ? char.IsDigit(input[current]) : false
                            ; current++, length++
                            )
                            ;

                        if (current < input.Length ? input[current] == '.' : false)
                        {
                            length++;
                            current++;
                            for (
                                ;
                                current < input.Length
                                    ? char.IsDigit(input[current]) && input[current] != ' '
                                    : false;
                                current++, length++
                            )
                                ;

                            tokens.Add(
                               new Token(start, input.Substring(start, length), TokenType.Literal)
                            );
                            current--;
                        }
                        else
                        {
                            tokens.Add(new(start, input.Substring(start, length), TokenType.Literal));
                            current--;
                        }
                    }
                    break;

                default:

                    {
                        if (char.IsLetter(input[current]) || input[current] == '_')
                        {
                            int start = current,
                                length = 0;
                            for (
                                ;
                                current < input.Length
                                    ? (
                                        char.IsLetterOrDigit(input[current])
                                        || input[current] == '_'
                                    )
                                        && input[current] != ' '
                                    : false;
                                current++, length++
                            )
                                ;
                            string word_ = input.Substring(start, length);
                            var tt = Token.GetTokenType(word_);
                            tokens.Add(new Token(start, input.Substring(start, length), tt));
                            current--;
                        }
                    }
                    break;
            }
        }
        tokens.Add(new Token(input.Length, "", TokenType.EOF));
        return tokens;
    }
}
