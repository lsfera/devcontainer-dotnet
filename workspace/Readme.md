# Exercise 1

Given an input array x where:

- the first element represents the numbers of available spots; 
- all subsequent elements are the occupied spots;

find all the adjacent spots given a two column layout. 

Example: 

given input array [12, 2, 5, 8, 12] 

then the visual representation is: 

| <!-- --> | <!-- --> |
|----------|----------|
|    1     |   X      |
|    3     |   4      |
|    X     |   6      |
|    7     |   X      |
|    9     |  10      |
|    11    |   X      |

Output:

- adjacent spots: [1, 3], [3, 4], [4, 6], [7, 9], [9, 10], [9, 11]

- combinations: 6

[Solution](./two_cols.cs)

# Exercise 2

Extend the previous solution to accomodate a three colum layout. 

Using the previous example, the visual representation is depicted below:

| <!-- --> | <!-- --> | <!-- --> |
|----------|----------|----------|
|     1    |     X    |     3    |  
|     4    |     X    |     6    |
|     7    |     X    |     9    |
|    10    |    11    |     X    |

Output:
- adjacent spots: [1, 4], [3, 6], [4, 7], [6, 9], [7, 10], [10, 11] 
- combinations: 6

[Solution](./three_cols.cs)

# Exercise 3

Extend the previous solution to accomodate a four column layout.

Using the previous example, the visual representation is depicted below:

| <!-- --> | <!-- --> | <!-- --> | <!-- --> |
|----------|----------|----------|----------|
|     1    |     X    |     3    |     4    |  
|     X    |     6    |     7    |     X    |
|     9    |    10    |    11    |     X    |

Output:
- adjacent spots: [3, 4], [3, 7], [6, 7], [6, 10], [7, 11], [9, 10], [10, 11] 
- combinations: 6

[Solution](./four_cols.cs)

# Exercise 4

Extend the previous solution to accomodate a parametric column layout.

The input array provides layout column as well in the following form:

- [[availableSpots, columns], [ occupiedSpots ]]; 

Using the previous examples the input parameter will be respectively:

- [[12, 2],[2, 5, 8, 12]]
- [[12, 3],[2, 5, 8, 12]]
- [[12, 4],[2, 5, 8, 12]]


[Solution](./parametric_version.cs)