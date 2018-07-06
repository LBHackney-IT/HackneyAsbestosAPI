# HackneyAsbestosAPI
API to retrieve asbestos information from Hackney's properties.

## List inspections for a property

Returns a list of inspections matching a property id.

```
GET /v1/inspection/
```

### Parameters

- inspectionId (required)

### Response

```json
{
  "results": [
    {
      "id":12345,
      "jobId":19044,
      "priorityAssessmentScore":null,
      "locationDescription":null,
      "quantity":null,
      "roomId":219892,
      ...etc...
    },
    {
      ...etc...
    }
  ]
}
```


### List room details

TODO

### List floor details

TODO


