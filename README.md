# Parsing SQL

This is the companion repository for the article [Parsing SQL](https://tomassetti.me/parsing-sql). This is a standard C# project created with the `dotnet` CLI. So, you can run it with `dotnet run`.

You need to generate the parser from the included grammar using ANTLR. If you do not know how to use you can read a tutorial on [setting up ANTLR](https://tomassetti.me/antlr-mega-tutorial/#setup-antlr).

To generate the parser you can use this command.

```
antlr4 SQL.g4 -Dlanguage=CSharp -o generated\ -encoding UTF-8
```

This will generate the parser inside the `generated` folder.

 The sample sql file in the data folder `sqlite-sakila-schema.sql` comes from [jooq](https://github.com/jOOQ/jOOQ/tree/master/jOOQ-examples/Sakila/sqlite-sakila-db) and it has a BSD License. The rest of the project uses the Apache 2.0 license.