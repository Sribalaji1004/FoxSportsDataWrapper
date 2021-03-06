﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoxSportsDataWrapper
{
    public class Enums
    {
        public enum EntryType
        {
            Group = 1, //A group.  The GroupID is defined in the EntryID field.
            Game, //A singular game.  The GameID is defined in the EntryID field
            TodaysGames, //All games for a given sport for today.  The sport is defined in the EntryID field.
            PreviousGames, //All games for a given sport prior to today.  The sport is defined in the EntryID field.
            Ads,
            HiveGame, //6
            HiveTodaysGames, //7
            HivePriorGames, //8
            GroupOfGames,
            TodaysGamesScheduleQuickRip, //10
            TodaysGamesInProgressQuickRip,
            TodaysGamesFinalsQuickRip, //12
            GamesScheduleCurrent, //13
            HiveTodaysGamesInProgressQuickRip, //14
            HiveTodaysGamesFinalsQuickRip,
            PreviousGamesFinalsQuickRip, //16
            HivePreviousGamesFinalsQuickRip,
            GroupofGamesQuickRip, //18
            HiveTodaysLeaderList, //19
            APRankings, //20
            GamesScheduleNext,//21
            Top25, //22
            Standings, //23
            CFPRankings, //24
            Top25GamesToday,// 25
            Top25GamesPrevious,// 26
            GameStats,//27
            WeeklyLeadersPassing, //28
            WeeklyLeadersRushing,// 29
            WeeklyLeadersRcvng, //30
            SpecificDate, //31
            TodaysGamesQuickPlayout, //32 - Esakki added new Entry Types on 29/08/2016 for SG-487
            GamesScheduleCurrentQuickRip , //33 - Esakki added new Entry Types ("Currentweek - QuickRip") option  on 09/01/2017 for NFL and CFB Leagues 
            GamesScheduleCurrentQuickPlayoutWithNotes, //34
            Top25GamesQuickPlayoutWithNotes //35
        };

    
     }
}