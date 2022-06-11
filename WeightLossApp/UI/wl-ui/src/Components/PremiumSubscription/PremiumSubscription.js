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

	// Called when update button is clicked
	updateClick() {
		// Sending HTTP PUT request to the server
		// with data from state
		fetch(constants.API_URL + "PremiumSubscription", {
			method: "PUT",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify({
				Id: this.state.itemID,
				PremiumDays: this.state.itemName,
				PremiumPrice: this.state.itemCalories,
			})
		})
			.then(res => res.json())
			.then(
				result => {
					this.refreshList();
				},
				error => {
					alert("Failed");
				}
			);
	}

	// Called when delete button is clicked
	deleteClick(id) {
		// Confirmation pop-up
		if (window.confirm("Are you sure?")) {
			// Sending request to delete item from DB with current id
			fetch(constants.API_URL + "PremiumSubscription/" + id, {
				method: "DELETE",
				headers: {
					"Content-Type": "application/json"
				}
			})
				.then(res => res.json())
				.then(
					result => {
						this.refreshList();
					},
					error => {
						alert("Failed");
					}
				);
		}
	}
}