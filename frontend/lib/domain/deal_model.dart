// To parse this JSON data, do
//
//     final dealModel = dealModelFromJson(jsonString);

import 'dart:convert';

DealModel dealModelFromJson(String str) => DealModel.fromJson(json.decode(str));

String dealModelToJson(DealModel data) => json.encode(data.toJson());

class DealModel {
  String dealId;
  String productId;
  double discount_percentage;

  DealModel({
    required this.dealId,
    required this.productId,
    required this.discount_percentage,
  });

  factory DealModel.fromJson(Map<String, dynamic> json) => DealModel(
        dealId: json["id"],
        productId: json["productid"],
        discount_percentage: json["discount_percentage"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "id": dealId,
        "productid": productId,
        "discount_percentage": discount_percentage,
      };
}
