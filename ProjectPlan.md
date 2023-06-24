# Code Louisville SD2 Project Plan

## Project Brief

A Shopping List console application that will allow users to create a shopping list. It will contain a database that is already seeded with some grocery items with their price and aisle location. It will also allow you to add grocery items to that database. Finally you will be able to get an approximate total price for your list and sort your list by aisle so you can shop quickly.

## Technical Description

I will be using Entity Framework Core to perform CRUD operations against a db table that will be storing grocery item entries.

### Table Structure

| Column Name | Data Type | Nullable |
| --- | --- | --- |
| Id | Int | No |
| Name | varchar(255) | No |
| Price | float | No |
| Aisle | Int | Yes |
| Section | varchar(10) | No |

## Requirements

- Create 3 or more unit tests for your application
- Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program
- Query your database using a raw SQL query, not EF