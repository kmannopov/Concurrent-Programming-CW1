using _8466.Domain.Dtos;
using _8466.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _8466.Infrastructure
{
    public static class Extensions
    {
        public static string GetRandomIpAddress()
        {
            var random = new Random();
            return $"{random.Next(1, 255)}.{random.Next(1, 255)}.{random.Next(1, 255)}.{random.Next(1, 255)}";
        }

        public static List<SwipeDto> ReturnAsSwipeList(this string swipes, string ipAddress)
        {
            return swipes.Split('\n').Select(s =>
            {
                var tmp = s.Split(',');
                return new SwipeDto(ipAddress, tmp[0], DateTime.Parse(tmp[1]), tmp[2]);
            }).ToList();
        }

        public static Swipe ReturnAsSwipe(this SwipeDto swipeDto)
        {
            return new Swipe
            {
                IpAddress = swipeDto.IpAddress,
                StudentId = swipeDto.StudentId,
                SwipeDate = swipeDto.Date,
                Direction = swipeDto.Direction
            };
        }
    }
}