# Exercise 1

Provide the function that amend a json string so that any property with value equal to "N\A", empty, blank string will be removed.
In casde of array simply remove the offending value.

Given th followong json:

{
    "name":{
        "first": "John",
        "middle": "", 
        "last": "Doe"
        },
    "age":25,
    "interests":["biking", "skiing", "N\A"],
    "education": " "
}

the resulting json will be:

{
    "name":{
        "first": "John",
        "last": "Doe"
        },
    "age":25,
    "interests":["biking", "skiing"],
}



[Solution](./app.cs)

```
cat << EOF | ./app.cs
{
    "name":{
        "first": "John",
        "middle": "",
        "last": "Doe"
        },
    "age":25,
    "interests":["biking", "skiing", "N\\\A"],
    "education": " "
}
EOF
```
