# HackneyAsbestosAPI
API to retrieve asbestos information from Hackney's properties.

## List inspections for a property

Returns a list of inspections matching a Universal Housing property id.

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


## List room details

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

## List floor details

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
    "id": 12345,
    "description": "Fifth",
    "propertyId": 12345,
    "orderId": null,
    "uprn": "00000000",
    "isInspected": true,
    "isDoesContainAsbestos": false,
    "isDidContainAsbestos": false,
    "isAnyToDos": false,
    "createdBy": "TEST",
    "modifiedBy": null,
    "dateOfModification": null,
    "dateOfCreation": "2018-01-25T10:20:56.66+00:00",
    "isActive": true,
    "isLiableToAsbestos": false
  }
}
```


## List element details

Returns an element matching an element id.

```
GET /v1/element/
```
### Parameters

- elementId (required)

### Response

```json
{
  "results": {
    "id": 12345,
    "roomId": 123456,
    "description": "Air Conditioning Unit",
    "propertyId": 000001,
    "orderId": null,
    "uprn": "00000001",
    "isInspected": true,
    "isDoesContainAsbestos": false,
    "isDidContainAsbestos": false,
    "isAnyToDos": false,
    "createdBy": "JOHN",
    "modifiedBy": null,
    "dateOfModification": null,
    "dateOfCreation": "2018-01-25T10:20:57.033+00:00",
    "isActive": true,
    "isLiableToAsbestos": false
  }
}

```


## Get photo

Returns a photo matching a photo id

```
GET /v1/document/photo/
```
### Parameters

- photoId (required)

### Response

An image in jpeg format.


## Get documents related to a photo

Returns a list of documents about photos for a Universal Housing property id.
The id of each of the documents returned corresponds to a photo id.

```
GET /v1/document/photo/:propertyId
```
### Parameters

- propertyId (required)

### Response

```json
{
  "results": [
    {
      "id": 12345,
      "reference": "0000000000-01",
      "description": "0000000000-01",
      "jobId": null,
      "propertyId": 012345,
      "uprn": "00000001",
      "createdBy": "JOHN",
      "modifiedBy": "MIKE",
      "dateOfModification": "2009-09-15T14:33:55.733+00:00",
      "dateOfCreation": "2009-09-15T14:33:55.733+00:00",
      "isApproved": true
    },
    {
      ...etc...
    }
  ]
}
```

## Get main photo

Returns a main photo matching a main photo id

```
GET /v1/document/mainphoto/
```
### Parameters

- mainPhotoId (required)

### Response

An image in jpeg format.


## Get documents related to a main photo

Returns a list of documents about main photos for a Universal Housing property id.
The id of each of the documents returned corresponds to a main photo id.

```
GET /v1/document/mainphoto/:propertyId
```
### Parameters

- propertyId (required)

### Response

```json
{
  "results": [
    {
      "id": 12345,
      "reference": "0000000000-01",
      "description": "0000000000-01",
      "jobId": null,
      "propertyId": 012345,
      "uprn": "00000001",
      "createdBy": "JOHN",
      "modifiedBy": "MIKE",
      "dateOfModification": "2009-09-15T14:33:55.733+00:00",
      "dateOfCreation": "2009-09-15T14:33:55.733+00:00",
      "isApproved": true
    },
    {
      ...etc...
    }
  ]
}
```

## Get drawing

Returns a drawing matching a drawing id

```
GET /v1/document/drawing/
```
### Parameters

- drawingId (required)

### Response

A floor plan in pdf or jpeg format.


## Get documents related to a drawing

Returns a list of documents about drawings for a Universal Housing property id.
The id of each of the documents returned corresponds to a drawing id.

```
GET /v1/document/drawing/:propertyId
```
### Parameters

- propertyId (required)

### Response

```json
{
  "results": [
    {
      "id": 12345,
      "reference": "0000000000-01",
      "description": "0000000000-01",
      "jobId": null,
      "propertyId": 012345,
      "uprn": "00000001",
      "createdBy": "JOHN",
      "modifiedBy": "MIKE",
      "dateOfModification": "2009-09-15T14:33:55.733+00:00",
      "dateOfCreation": "2009-09-15T14:33:55.733+00:00",
      "isApproved": true
    },
    {
      ...etc...
    }
  ]
}
```

## Get report

Returns a report matching a report id

```
GET /v1/document/report/
```
### Parameters

- reportId (required)

### Response

A report in pdf format.


## Get documents related to a report

Returns a list of documents about reports for a Universal Housing property id.
The id of each of the documents returned corresponds to a report id.

```
GET /v1/document/report/:propertyId
```
### Parameters

- propertyId (required)

### Response

```json
{
  "results": [
    {
      "id": 12345,
      "reference": "0000000000-01",
      "description": "0000000000-01",
      "jobId": null,
      "propertyId": 012345,
      "uprn": "00000001",
      "createdBy": "JOHN",
      "modifiedBy": "MIKE",
      "dateOfModification": "2009-09-15T14:33:55.733+00:00",
      "dateOfCreation": "2009-09-15T14:33:55.733+00:00",
      "isApproved": true
    },
    {
      ...etc...
    }
  ]
}
```
