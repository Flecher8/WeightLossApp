import React, { Component } from "react";
import { constants } from "../../Constants";
import BootstrapTable from "react-bootstrap-table-next";
import filterFactory, { textFilter, numberFilter } from "react-bootstrap-table2-filter";
import paginationFactory from "react-bootstrap-table2-paginator";

export class PremiumSubscription extends Component {
	// Main constructor
	constructor(props) {
		super(props);

		// State initialization
		// Component state - properties of this component
		this.state = {
			// List of data to be displayed
			PremiumSubscription: [],
            DesignTheme: [],
			// Title of the modal window
			modalTitle: "",

			// Data of the PremiumSubscription item
			// This data will be displayed in the modal window
			premiumID: 0,
			premiumDays: 0,
			premiumPrice: 0.0,
		};
	}

	// Called on Add Button click
	addPremiumClick() {
		// Clearing item data and saving current state
		this.setState({
			modalTitle: "Adding new premium subscription",

			premiumID: 0,
			premiumDays: 0,
			premiumPrice: 0.0,

		});
	}

	// Called when create button is clicked
	createClick() {
		// Sending HTTP POST request to the server
		// with data from state
		fetch(constants.API_URL + "PremiumSubscription", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			// Content of the request, will be converted to
			// the PremiumSubscription instance on API side
			// and passed to HTTP Post method
			body: JSON.stringify({
				PremiumDays: this.state.itemName,
				PremiumPrice: this.state.itemCalories,
				Proteins: this.state.itemProteins
		})})
			.then(res => res.json())
			.then(
				result => {
					// Refreshing data
					this.refreshList();
				},
				error => {
					alert("Failed");
				}
			);
	}
}