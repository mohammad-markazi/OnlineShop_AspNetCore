﻿using System.Collections.Generic;

namespace _01_LampShadeQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> GetLatestArticles();

        ArticleQueryModel GetArticleDetail(string slug);
    }
}