using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using LBHAsbestosAPI.Repository;
using LBHAsbestosAPI.Services;
using System.Linq;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Dtos;
using UnitTests.MoqClasses;

namespace UnitTests
{
	public class AsbestosServiceTest// : IClassFixture<InitFixture>
    {
		//InitFixture _fixture;

		//public AsbestosServiceTest (InitFixture fixture)
		//{
		//	_fixture = fixture;
		//}
        
		[Fact]
        public void Getinspections_should_return_a_list_of_inspectiondtos()
		{
			var mockPSIApi = new MoqPSIApi();
			var mockAsbestosService = new Mock<AsbestosService>(mockPSIApi._api.Object);
			var result = mockAsbestosService.Object.GetInspections("123456");
			Assert.IsType<EnumerableQuery<InspectionDto>>(result);
		}


    }

	//public class InitFixture
  //  {
  //      public InitFixture()
  //      {
		//	//AutoMapper.Mapper.Initialize(config =>
		//	//{
		//	//    config.CreateMap<Movie, MovieDto>();
		//	//});
		//	mockPSIApi = new Mock<PSIApi>();
		//	mockPSIApi = SetupMockAPI(mockPSIApi);

  //      }
      
		//public Mock<PSIApi> SetupMockAPI(Mock<PSIApi> api)
  //      {
		//	var inspections = new List<Inspection>()
		//	{
		//		new Inspection()
		//		{
  //                  Id = 236977,
  //                  JobId = 19044,
  //                  PriorityAssessmentScore = null,
  //                  LocationDescription = null,
  //                  Quantity = null,
  //                  FloorId = 36685,
  //                  RoomId = 219892,
  //                  ElementId = 254773,
  //                  Material = null,
  //                  MaterialScore = null,
  //                  MaterialType = null,
  //                  MaterialTypeScore = null,
  //                  Measurement = null,
  //                  Position = null,
  //                  PositionScore = null,
  //                  Friability = null,
  //                  FriabilityScore = null,
  //                  Access = null,
  //                  AccessScore = null,
  //                  PhotoId = null,
  //                  DrawingId = null,
  //                  Condition = null,
  //                  ConditionScore = null,
  //                  InspectionNumber = 1,
  //                  InspectionType = "Sample Taken",
  //                  InspectionTypeScore = null,
  //                  Protection = null,
  //                  ProtectionScore = null,
  //                  InspectionResult = "Asbestos Removed",
  //                  InspectionResultScore = null,
  //                  InspectionScore = null,
  //                  MaterialAssessmentScore = null,
  //                  InspectionScoreCategory = null,
  //                  QuantityLeft = null,
		//			DateOfInspection = DateTime.Now,
  //                  InspectedBy = null,
  //                  PropertyId = 375209,
  //                  Field1 = null,
  //                  Field1Score = null,
  //                  Field2 = null,
  //                  Field2Score = null,
  //                  Field3 = null,
  //                  Field3Score = null,
  //                  Field4 = null,
  //                  Field4Score = null,
  //                  Field5 = null,
  //                  Field5Score = null,
  //                  Field6 = null,
  //                  Field6Score = null,
  //                  Field7 = null,
  //                  Field7Score = null,
  //                  Field8 = null,
  //                  Field8Score = null,
  //                  Field9 = null,
  //                  Field9Score = null,
  //                  Text1 = null,
  //                  DtmDate1 = null,
  //                  Comment = null,
  //                  InspectionId = "00000021INS-1",
  //                  PreviousInspectionId = null,
  //                  IsLatestInspection = true,
  //                  Text2 = null,
  //                  ExposedGroup1 = null,
  //                  ExposedGroup1Score = null,
  //                  ExposedGroup2 = null,
  //                  ExposedGroup2Score = null,
  //                  ExposedGroup3 = null,
  //                  ExposedGroup3Score = null,
  //                  Uprn = 00000021,
  //                  OverAllAnalysisResult = "Amphibole Asbestos Excluding Crocidolite",
  //                  OverAllAnalysisResultScore = 2,
  //                  RecommendationAction1 = null,
  //                  RecommendationPriority1 = null,
  //                  RecommendedBy1 = null,
  //                  RecommendationAction2 = null,
  //                  RecommendationPriority2 = null,
  //                  RecommendedBy2 = null,
  //                  CreatedBy = "DIEU66",
  //                  ModifiedBy = null,
  //                  DateOfModification = null,
		//			DateOfCreation = DateTime.Now,
  //                  IsApproved = true
  //              },
		//		new Inspection()
  //              {
  //                  Id = 236978,
  //                  JobId = 19044,
  //                  PriorityAssessmentScore = null,
  //                  LocationDescription = null,
  //                  Quantity = null,
  //                  FloorId = 36687,
  //                  RoomId = 219893,
  //                  ElementId = 254774,
  //                  Material = null,
  //                  MaterialScore = 0,
  //                  MaterialType = null,
  //                  MaterialTypeScore = 0,
  //                  Measurement = null,
  //                  Position = null,
  //                  PositionScore = 0,
  //                  Friability = null,
  //                  FriabilityScore = 0,
  //                  Access = null,
  //                  AccessScore = 0,
  //                  PhotoId = null,
  //                  DrawingId = null,
  //                  Condition = null,
  //                  ConditionScore = 0,
  //                  InspectionNumber = 3,
  //                  InspectionType = "Visual Inspection",
  //                  InspectionTypeScore = 0         ,
  //                  Protection = null,
  //                  ProtectionScore = 0,
		//			InspectionResult = "Asbestos Identified",
  //                  InspectionResultScore = 2         ,
  //                  InspectionScore = null,
  //                  MaterialAssessmentScore = 0,
  //                  InspectionScoreCategory = null,
  //                  QuantityLeft = null,
		//			DateOfInspection = DateTime.Now,
  //                  InspectedBy = null,
  //                  PropertyId = 375209,
  //                  Field1 = null,
  //                  Field1Score = 0,
  //                  Field2 = null,
  //                  Field2Score = 0,
  //                  Field3 = null,
  //                  Field3Score = 0,
  //                  Field4 = null,
  //                  Field4Score = 0,
  //                  Field5 = null,
  //                  Field5Score = 0,
  //                  Field6 = null,
  //                  Field6Score = 0,
  //                  Field7 = null,
  //                  Field7Score = 0,
  //                  Field8 = null,
  //                  Field8Score = 0,
  //                  Field9 = null,
  //                  Field9Score = 0,
  //                  Text1 = null,
  //                  DtmDate1 = null,
  //                  Comment = null,
		//			InspectionId = "00000021INS-3",
  //                  PreviousInspectionId = null,
  //                  IsLatestInspection = true,
  //                  Text2 = null,
  //                  ExposedGroup1 = null,
  //                  ExposedGroup1Score = null,
  //                  ExposedGroup2 = null,
  //                  ExposedGroup2Score = null,
  //                  ExposedGroup3 = null,
  //                  ExposedGroup3Score = null,
  //                  Uprn = 00000021,
  //                  OverAllAnalysisResult = null,
  //                  OverAllAnalysisResultScore = 0,
  //                  RecommendationAction1 = null,
  //                  RecommendationPriority1 = null,
		//			RecommendedBy1 = "Caretaker",
  //                  RecommendationAction2 = null,
  //                  RecommendationPriority2 = null,
  //                  RecommendedBy2 = null,
		//			CreatedBy = "DIEU66",
  //                  ModifiedBy = null,
  //                  DateOfModification = null,
		//			DateOfCreation = DateTime.Now,
  //                  IsApproved = true
  //              },
		//		new Inspection()
  //              {
  //                  Id = 236979,
  //                  JobId = 19044,
  //                  PriorityAssessmentScore = null,
  //                  LocationDescription = null,
  //                  Quantity = null,
  //                  FloorId = 36686,
  //                  RoomId = 219891,
  //                  ElementId = 254775,
  //                  Material = null,
  //                  MaterialScore = null,
  //                  MaterialType = null,
  //                  MaterialTypeScore = null,
  //                  Measurement = null,
  //                  Position = null,
  //                  PositionScore = null,
  //                  Friability = null,
  //                  FriabilityScore = null,
  //                  Access = null,
  //                  AccessScore = null,
  //                  PhotoId = null,
  //                  DrawingId = null,
  //                  Condition = null,
  //                  ConditionScore = null,
  //                  InspectionNumber = 2,
		//			InspectionType = "Sample Taken",
  //                  InspectionTypeScore = null,
  //                  Protection = null,
  //                  ProtectionScore = null,
		//			InspectionResult = "Asbestos Removed",
  //                  InspectionResultScore = null,
  //                  InspectionScore = null,
  //                  MaterialAssessmentScore = null,
  //                  InspectionScoreCategory = null,
  //                  QuantityLeft = null,
		//			DateOfInspection = DateTime.Now,
  //                  InspectedBy = null,
  //                  PropertyId = 375209,
  //                  Field1 = null,
  //                  Field1Score = null,
  //                  Field2 = null,
  //                  Field2Score = null,
  //                  Field3 = null,
  //                  Field3Score = null,
  //                  Field4 = null,
  //                  Field4Score = null,
  //                  Field5 = null,
  //                  Field5Score = null,
  //                  Field6 = null,
  //                  Field6Score = null,
  //                  Field7 = null,
  //                  Field7Score = null,
  //                  Field8 = null,
  //                  Field8Score = null,
  //                  Field9 = null,
  //                  Field9Score = null,
  //                  Text1 = null,
  //                  DtmDate1 = null,
  //                  Comment = null,
  //                  InspectionId = "00000021INS-2",
  //                  PreviousInspectionId = null,
  //                  IsLatestInspection = true,
  //                  Text2 = null,
  //                  ExposedGroup1 = null,
  //                  ExposedGroup1Score = null,
  //                  ExposedGroup2 = null,
  //                  ExposedGroup2Score = null,
  //                  ExposedGroup3 = null,
  //                  ExposedGroup3Score = null,
  //                  Uprn = 00000021,
		//			OverAllAnalysisResult = "Crocidolite",
  //                  OverAllAnalysisResultScore = 3,
  //                  RecommendationAction1 = null,
  //                  RecommendationPriority1 = null,
		//			RecommendedBy1 = "Specialist",
  //                  RecommendationAction2 = null,
  //                  RecommendationPriority2 = null,
  //                  RecommendedBy2 = null,
		//			CreatedBy = "DIEU66",
  //                  ModifiedBy = null,
  //                  DateOfModification = null,
		//			DateOfCreation = DateTime.Now,
  //                  IsApproved = true
  //              }
		//	}.AsQueryable();

		//	api.Setup(a => a.GetInspections("123456")).Returns(inspections);
  //         return api;
  //      }
		//public Mock<PSIApi> mockPSIApi { get; private set; }
    
}
                             