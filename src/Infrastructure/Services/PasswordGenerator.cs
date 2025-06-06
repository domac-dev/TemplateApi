﻿using Domain.Abstraction.Security;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public PasswordGenerator()
        {
            Length = 8;
            MinLowercases = 1;
            MinUppercases = 1;
            MinDigits = 1;
            MinSpecials = 1;
        }

        private enum CharType
        {
            Lowercase,
            Uppercase,
            Digit,
            Special
        }

        private readonly Dictionary<CharType, string> _chars = new Dictionary<CharType, string>()
        {
            { CharType.Lowercase, "abcdefghijklmnopqrstuvwxyz" },
            { CharType.Uppercase, "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
            { CharType.Digit, "0123456789" },
            { CharType.Special, "!@#$%^&*()-_=+{}[]?<>.," }
        };

        private Dictionary<CharType, int> _outstandingChars = new Dictionary<CharType, int>();

        public int Length { get; set; }
        public int MinLowercases { get; set; }
        public int MinUppercases { get; set; }
        public int MinDigits { get; set; }
        public int MinSpecials { get; set; }

        private void ResetOutstandings()
        {
            _outstandingChars[CharType.Lowercase] = MinLowercases;
            _outstandingChars[CharType.Uppercase] = MinUppercases;
            _outstandingChars[CharType.Digit] = MinDigits;
            _outstandingChars[CharType.Special] = MinSpecials;
        }

        private void DecreaseOustanding(CharType type)
        {
            if (_outstandingChars[type] > 0)
            {
                _outstandingChars[type]--;
            }
        }

        private char DrawChar(params CharType[] types)
        {
            var filteredChars = types.Length == 0 ? _chars : _chars.Where(x => types.Contains(x.Key));
            int length = filteredChars.Sum(x => x.Value.Length);
            int index = RandomNumberGenerator.GetInt32(length);
            int offset = 0;

            foreach (var item in filteredChars)
            {
                if (index < offset + item.Value.Length)
                {
                    DecreaseOustanding(item.Key);
                    return item.Value[index - offset];
                }
                offset += item.Value.Length;
            }

            return new char();
        }

        public string Generate()
        {
            if (Length < MinLowercases + MinUppercases + MinDigits + MinSpecials)
            {
                throw new ArgumentException("Minimum requirements exceed password length.");
            }

            ResetOutstandings();

            var password = new StringBuilder();

            for (int i = 0; i < Length; i++)
            {
                if (_outstandingChars.Sum(x => x.Value) == Length - i)
                {
                    var outstanding = _outstandingChars.Where(x => x.Value > 0).Select(x => x.Key).ToArray();
                    password.Append(DrawChar(outstanding));
                }
                else
                {
                    password.Append(DrawChar());
                }
            }

            return password.ToString();
        }
    }

}
