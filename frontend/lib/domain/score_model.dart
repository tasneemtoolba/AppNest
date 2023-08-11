import 'dart:convert';

ScoreModel scoreModelFromJson(String str) =>
    ScoreModel.fromJson(json.decode(str));

class ScoreModel {
  String productId;
  String id;
  String dealId;
  String productName;
  int score;
  // double dealPercentage;

  ScoreModel({
    required this.id,
    required this.productId,
    required this.dealId,
    required this.productName,
    required this.score,
    // required this.dealPercentage,
  });

  factory ScoreModel.fromJson(Map<String, dynamic> json) => ScoreModel(
        id: json["id"],
        productId: json["productid"],
        dealId: json["dealid"],
        productName: json["product_name"],
        score: json["score_value"],
        // dealPercentage: json["deal_percentage"]?.toDouble(),
      );
}
