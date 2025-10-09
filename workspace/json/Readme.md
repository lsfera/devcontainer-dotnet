# Exercise 1

Provide the function that amend a json string so that any property with value equal to "N\A", empty or blank string will be removed.
In casde of array simply remove the offending value.

Given th followong json:

```
{
    "intarr":[1,2,3],
    "name":{
        "first": "John",
        "middle": "", 
        "last": "Doe"
        },
    "age":25,
    "interests":["biking", "skiing", "N\A"],
    "education": " "
}
```

the resulting json will be:

```
{
    "intarr":[1,2,3],
    "name":{
        "first": "John",
        "last": "Doe"
        },
    "age":25,
    "interests":["biking", "skiing"],
}
```

Solution [./app.cs](./app.cs):   using Newtonsoft.Json

Solution [./app2.cs](./app2.cs): using System.Text.Json

```
declare -a arr=("./app.cs" "./app2.cs")
for i in "${arr[@]}"
do
echo "solution for: $i"
cat << EOF | $i
{
    "intarr":[1,2,3],
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
done
```

