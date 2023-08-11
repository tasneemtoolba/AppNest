import 'package:appnest/api.dart';
import 'package:appnest/domain/deal_model.dart';
import 'package:appnest/domain/product_model.dart';
import 'package:flutter/material.dart';
import 'package:uuid/uuid.dart';

class ViewProduct extends StatefulWidget {
  const ViewProduct({Key? key, required this.product}) : super(key: key);
  final ProductModel product;
  @override
  State<ViewProduct> createState() => _ViewProductState();
}

class _ViewProductState extends State<ViewProduct> {
  final _formKey = GlobalKey<FormState>();
  TextEditingController dealPercentage = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Deals'),
      ),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(15),
          child: Column(mainAxisSize: MainAxisSize.min, children: [
            ElevatedButton(
              onPressed: () {
                showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return AlertDialog(
                        content: Stack(
                          clipBehavior: Clip.none,
                          children: <Widget>[
                            Positioned(
                              right: -40.0,
                              top: -40.0,
                              child: InkResponse(
                                onTap: () {
                                  Navigator.of(context).pop();
                                },
                                child: const CircleAvatar(
                                  backgroundColor: Colors.red,
                                  child: Icon(Icons.close),
                                ),
                              ),
                            ),
                            Form(
                              key: _formKey,
                              child: Column(
                                mainAxisSize: MainAxisSize.min,
                                children: <Widget>[
                                  Padding(
                                    padding: const EdgeInsets.all(8.0),
                                    child: TextFormField(
                                      controller: dealPercentage,
                                      keyboardType: TextInputType.number,
                                      validator: (value) =>
                                          int.tryParse(value!) == null
                                              ? 'please enter a number'
                                              : null,
                                    ),
                                  ),
                                  Padding(
                                    padding: const EdgeInsets.all(8.0),
                                    child: ElevatedButton(
                                      child: const Text("Add Deal"),
                                      onPressed: () async {
                                        if (_formKey.currentState!.validate()) {
                                          DealModel deal = DealModel(
                                              dealId: const Uuid().v1(),
                                              productId: widget.product.id,
                                              discount_percentage: double.parse(
                                                  dealPercentage.text));
                                          Navigator.of(context).pop();
                                          await API().getDeals();
                                          await API().addDeal(deal);
                                          setState(() {});
                                        }
                                      },
                                    ),
                                  )
                                ],
                              ),
                            ),
                          ],
                        ),
                      );
                    });
              },
              child: const Text('Add Deal'),
            ),
            Expanded(
              child: FutureBuilder(
                future: API().getDealsByProductId(widget.product.id),
                builder: (BuildContext context, AsyncSnapshot snapshot) {
                  if (snapshot.hasData) {
                    List<DealModel> deals = snapshot.data;
                    print("number of deals = ${deals.length}");
                    return deals.length != 0
                        ? ListView.builder(
                            itemBuilder: (context, index) {
                              print("deals in future : ${snapshot.data}");
                              return ListTile(
                                onTap: () {},
                                title: Text(widget.product.productName),
                                subtitle: Text(deals[index]
                                    .discount_percentage
                                    .toString()),
                              );
                            },
                            // verify if cardDetails is null to prevent app crash
                            itemCount: deals.length,
                            scrollDirection: Axis.vertical,
                            // controller: _controller,
                            shrinkWrap: true,
                          )
                        : const Center(child: Text("No Deals created yet"));
                    ;
                  } else if (snapshot.hasError) {
                    print("Error here in snapshot ${snapshot.error}");
                    return const Center(child: Text("An error has occurred"));
                  } else {
                    return const Center(child: CircularProgressIndicator());
                  }
                },
              ),
            )
          ]),
        ),
      ),
    );
  }
}
