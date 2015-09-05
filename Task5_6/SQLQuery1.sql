--1)Вывести список работников, зарплата которых ниже 300 $ и которые работают во Львове. 
SELECT [FirstName], [LastName] 
FROM [AdditionTask1].[dbo].[Empl]
WHERE [DepId] = 
 (SELECT [id] FROM [AdditionTask1].[dbo].[Dept] WHERE [City] = 'Львів') 
 AND [Salary] < 420

/* SELECT [FirstName], [LastName] 
 FROM [AdditionTask1].[dbo].[Empl]
 INNER JOIN [AdditionTask1].[dbo].[Dept]
 ON [Empl].[DepId] = [Dept].[id]
 WHERE [City] = 'Львів' AND [Salary] < 420*/

 --2)Вывести список имен работников (без повторов, упорядочен по алфавиту).
 SELECT DISTINCT [FirstName] 
 FROM [AdditionTask1].[dbo].[Empl]
 ORDER BY [FirstName] 

--3)Посчитать сколько работников у компании.
 SELECT COUNT(*) as emp_count 
 FROM [AdditionTask1].[dbo].[Empl]

 --4)Получить список количества работников в подразделении № 5
 SELECT COUNT(*) as emp5_count 
 FROM [AdditionTask1].[dbo].[Empl]
 WHERE [DepId] = 5

 --5)Получить список количества работников в каждом подразделении
 SELECT COUNT(*) as emp_count_in_dept
 FROM [AdditionTask1].[dbo].[Empl]
 GROUP BY [DepId]

 --6)Выберите список городов, содержащие букву 'Л' в начале слова.
 SELECT [City] 
 FROM [AdditionTask1].[dbo].[Dept]
 WHERE [City] LIKE 'Л%'

 --7)Выберите список всех городов, содержащие букву 'ь' в середине слова и количество работников более 20.
SELECT [City]
  FROM [AdditionTask1].[dbo].[Dept] 
  INNER JOIN [AdditionTask1].[dbo].[Empl] 
  ON [Dept].[id] = [Empl].[DepId]
  WHERE [City] LIKE '%ь%' 
  GROUP BY [City]
  HAVING COUNT([Empl].id) > 20;

--8)Выберите список людей, имеющих однофамильцев.
SELECT [LastName] 
FROM [AdditionTask1].[dbo].[Empl]
GROUP BY [LastName] 
HAVING COUNT([LastName]) > 1

--9)Выберите список людей, имеющих несколько полных совпадений имени 
  --и фамилии, которые, к тому же, работают  в городе 'Львов'. 
  SELECT *
  FROM [AdditionTask1].[dbo].[Dept] 
  INNER JOIN [AdditionTask1].[dbo].[Empl]
  ON [Dept].[id] = [Empl].[DepId]
  WHERE [FirstName]+[LastName] IN
   (SELECT [FirstName]+[LastName]
    FROM [AdditionTask1].[dbo].[Empl]
    GROUP BY [FirstName],[LastName] 
    HAVING COUNT([LastName]) > 1)
	AND ([City] LIKE 'Л%')
  

  --10)Получить список городов с количеством работников с именем 'Иван' более 10.
  SELECT [City]
  FROM [AdditionTask1].[dbo].[Dept]
  INNER JOIN [AdditionTask1].[dbo].[Empl]
  ON [Dept].[id] = [Empl].[DepId]
  WHERE [FirstName] = 'Іван'
  GROUP BY [City]
  HAVING COUNT([Empl].id) > 10


  --11) Получить список работников с фамилией, начинающейся на букву А.
  SELECT [FirstName], [LastName] 
  FROM [AdditionTask1].[dbo].[Empl] 
  WHERE [LastName] LIKE 'А%'

-- 12) Получить список работников с заработной платой выше 400 $.
SELECT *
FROM [AdditionTask1].[dbo].[Empl]
WHERE [Salary] > 400

--13)Получить средний размер заработной платы на фирме.
SELECT AVG([Salary]) as avg_salary
FROM [AdditionTask1].[dbo].[Empl]

--14)Получить список сотрудников, получающих заработную плату в диапазоне от 200 до 400 $
SELECT * 
FROM [AdditionTask1].[dbo].[Empl] 
WHERE [Salary] BETWEEN 200 AND 400

--15)Вывести “id” подразделения, в котором сотрудники получают максимальную заработную плату.
/*SELECT [Dept].[Id]
FROM [Empl] Join [Dept] on ([Dept].[Id] = [Empl].[DepId])
GROUP BY [Dept].[Id]*/

--SELECT MAX(subquery1.avg_salary) max_avg
--FROM (SELECT [DepId], AVG([Salary]) AS avg_salary FROM [dbo].[Empl] GROUP BY [DepId])subquery1
SELECT [DepId]  
FROM [dbo].[Empl] 
GROUP BY [DepId]
HAVING (AVG([Salary]) = (
						  SELECT  MAX(subquery.avg_salary) as max_avg
						  FROM (
							     SELECT [DepId], AVG([Salary]) AS avg_salary 
							     FROM [dbo].[Empl] 
							     GROUP BY [DepId]
								)subquery
			) 
		)

