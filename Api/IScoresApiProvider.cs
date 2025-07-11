﻿using FootballScoresApi.Model;

namespace FootballScoresApi.Api
{
    public interface IScoresApiProvider
    {
        Task<List<TeamData>> GetAllStandings(int season);
        Task<Fixture> TryGetFixtureByDate(string teamName, DateTime dateTime);
        Task<List<Fixture>> TryGetLastFixtures(string teamName, int numberOfMatches, int season);
    }
}