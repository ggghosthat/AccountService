using AccountService.Entity;
using AccountService.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace AccountService.Repository.Extenssions;

public static class UserRepositoryExtenssion
{
    public static IQueryable<User> Filter(this IQueryable<User> users, UserParameters userParameters)
    {
        var userParam = Expression.Parameter(typeof(User), "user");
        Expression predicate = Expression.Constant(true);

         // Build the expression based on provided parameters
        if (!string.IsNullOrEmpty(userParameters.FirstName))
        {
            var firstNameProperty = Expression.Property(userParam, nameof(User.FirstName));
            var firstNameValue = Expression.Constant(userParameters.FirstName);
            var firstNameEquals = Expression.Equal(firstNameProperty, firstNameValue);
            predicate = Expression.AndAlso(predicate, firstNameEquals);
        }

        if (!string.IsNullOrEmpty(userParameters.LastName))
        {
            var lastNameProperty = Expression.Property(userParam, nameof(User.LastName));
            var lastNameValue = Expression.Constant(userParameters.LastName);
            var lastNameEquals = Expression.Equal(lastNameProperty, lastNameValue);
            predicate = Expression.AndAlso(predicate, lastNameEquals);
        }

        // Add similar checks for MiddleName, Phone, and Email...
        if (!string.IsNullOrEmpty(userParameters.MiddleName))
        {
            var middleNameProperty = Expression.Property(userParam, nameof(User.MiddleName));
            var middleNameValue = Expression.Constant(userParameters.MiddleName);
            var middleNameEquals = Expression.Equal(middleNameProperty, middleNameValue);
            predicate = Expression.AndAlso(predicate, middleNameEquals);
        }

        if (!string.IsNullOrEmpty(userParameters.Phone))
        {
            var phoneProperty = Expression.Property(userParam, nameof(User.Phone));
            var phoneValue = Expression.Constant(userParameters.Phone);
            var phoneEquals = Expression.Equal(phoneProperty, phoneValue);
            predicate = Expression.AndAlso(predicate, phoneEquals);
        }

        if (!string.IsNullOrEmpty(userParameters.Email))
        {
            var emailProperty = Expression.Property(userParam, nameof(User.Email));
            var emailValue = Expression.Constant(userParameters.Email);
            var emailEquals = Expression.Equal(emailProperty, emailValue);
            predicate = Expression.AndAlso(predicate, emailEquals);
        }

        // Create the lambda expression
        var expression = Expression.Lambda<Func<User, bool>>(predicate, userParam);

        return users.Where(expression).Select(i => i);
    }
}