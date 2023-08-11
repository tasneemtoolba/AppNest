// To parse this JSON data, do
//
//     final dealModel = dealModelFromJson(jsonString);

import 'dart:convert';

DealModel dealModelFromJson(String str) => DealModel.fromJson(json.decode(str));

String dealModelToJson(DealModel data) => json.encode(data.toJson());

class DealModel {
  String dealId;
  String productId;
  double dealPercentage;

  DealModel({
    required this.dealId,
    required this.productId,
    required this.dealPercentage,
  });

  factory DealModel.fromJson(Map<String, dynamic> json) => DealModel(
        dealId: json["id"],
        productId: json["Productid"],
        dealPercentage: json["deal_percentage"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "id": dealId,
        "Productid": productId,
        "deal_percentage": dealPercentage,
      };
}
