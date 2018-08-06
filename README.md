# HackneyAsbestosAPI
API to retrieve asbestos information from Hackney's properties.

## List inspections for a property

Returns a list of inspections matching a property id.

```
GET /v1/inspection/
```

### Parameters

- propertyId (required)

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

Returns a room matching a room id.

```
GET /v1/room/
```
### Parameters

- roomId (required)

### Response

```json
{
  "results": {
    "id": 12345,
    "floorId": 1234,
    "description": "External",
    "propertyId": 123456,
    "orderId": null,
    "uprn": 123456,
    "isInspected": false,
    "isDoesContainAsbestos": false,
    "isDidContainAsbestos": false,
    "isAnyToDos": true,
    "createdBy": "TEST",
    "modifiedBy": null,
    "dateOfModification": null,
    "dateOfCreation": "2011-02-16T16:29:10.107+00:00",
    "isActive": true,
    "isLiableToAsbestos": true
  }
}
```

### List floor details

Returns a floor matching a floor id.

```
GET /v1/floor/
```
### Parameters

- floorId (required)

### Response

```json
{
  "results": {
    "Id": 12345,
    "Description": "Fifth",
    "PropertyId": 12345,
    "OrderId": null,
    "Uprn": "00000000",
    "IsInspected": true,
    "IsDoesContainAsbestos": false,
    "IsDidContainAsbestos": false,
    "IsAnyToDos": false,
    "CreatedBy": "TEST",
    "ModifiedBy": null,
    "DateOfModification": null,
    "DateOfCreation": "2018-01-25T10:20:56.66+00:00",
    "IsActive": true,
    "IsLiableToAsbestos": false
  }
}
```


