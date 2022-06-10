import React, { Component } from "react";
import { constants } from "../../Constants";
import BootstrapTable from "react-bootstrap-table-next";
import filterFactory, { textFilter, numberFilter } from "react-bootstrap-table2-filter";
import paginationFactory from "react-bootstrap-table2-paginator";

export class StoreData extends Component {
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

            // Data of the DesignThemeData item
			// This data will be displayed in the modal window
			themeID: 0,
			themeImage: "",
			themeBaseColor: "",
			themeAccentColor: "",
			themeSecondaryColor: "",
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

	// Called on Add Button click
	addThemeClick() {
		// Clearing item data and saving current state
		this.setState({
			modalTitle: "Adding new design theme",

			themeID: 0,
			themeImage: "",
			themeBaseColor: "",
			themeAccentColor: "",
			themeSecondaryColor: "",
		});
	}
}