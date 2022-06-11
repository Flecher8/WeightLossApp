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
			premiumSubscription: [],

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
				NumberOfDays: this.state.premiumDays,
				Price: this.state.premiumPrice,
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
				Id: this.state.premiumID,
				NumberOfDays: this.state.premiumDays,
				Price: this.state.premiumPrice,
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

	// HTTP GET request, retrieves data from server
	// and saves it to the component state
	refreshList() {
		fetch(constants.API_URL + "PremiumSubscription")
			.then(response => response.json())
			.then(data => {
				this.setState({ premiumSubscription: data });
			});
	}

	// Don't exactly know what is it :)
	// Maybe it is called when component is rendered ¯\_(ツ)_/¯
	componentDidMount() {
		this.refreshList();
	}

	// Next 2 functions called when user types
	// something in input fields of modal window.
	// They are saving this data to specified state variables
	changeDays = e => {
		this.setState({ premiumDays: e.target.value });
	};

	changePrice = e => {
		this.setState({ premiumPrice: e.target.value });
	};

	render() {
		// Object that describes selection of rows
		const selectRow = {
			mode: "radio",
			clickToSelect: true,
			style: { backgroundColor: "#f6f6f6" },
			onSelect: (row, isSelect, rowIndex, e) => {
				this.setState({
					modalTitle: "Editing Ingridient",
					premiumID: row.Id,
					premiumDays: row.Days,
					premiumPrice: row.Price,
				});
			}
		};

		// Array of objects, that describes
		// columns of the datatable
		const columns = [
			{
				dataField: "Id",
				sort: true,
				text: "Ingridient ID",
				headerAlign: "left"
			},
			{
				// Data field name
				dataField: "Days",
				// Header
				text: "Days",
				// Sortable
				sort: true,
				// Filtrable, and which Filter uses
				filter: numberFilter(),
				headerAlign: "left"
			},
			{
				dataField: "Price",
				text: "Price",
				sort: true,
				filter: numberFilter(),
				headerAlign: "left"
			}
		];

		// This part describes what will be displayed
		return (
			<div className="container">
				<div style={{ width: 80 + "vw" }}>
					<h3 className="m-5">This is Premium subscription page</h3>

					<BootstrapTable
						keyField="Id"
						data={this.state.premiumSubscription}
						columns={columns}
						filter={filterFactory()}
						filterPosition="top"
						// Pagination divides table into pages
						pagination={paginationFactory()}
						selectRow={selectRow}
					/>

					{/* Three buttons to perform basic operations */}
					<button
						type="button"
						className="btn btn-dark m-2 float-end"
						// Click will trigger modal
						data-bs-toggle="modal"
						// Id of modal to be triggered
						data-bs-target="#exampleModal"
						onClick={() => this.addPremiumClick()}>
						Add subscription
					</button>

					<button
						type="button"
						className="btn btn-dark m-2 float-end"
						data-bs-toggle="modal"
						data-bs-target="#exampleModal"
						// When there is no selected item,
						// button should be disabled
						disabled={this.state.premiumID === 0}>
						Edit subscription 
					</button>

					<button
						type="button"
						className="btn btn-dark m-2 float-end"
						onClick={() => this.deleteClick(this.state.premiumID)}
						disabled={this.state.premiumID === 0}>
						Delete subscription 
					</button>

					{/* Modal window component */}
					<div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
						<div className="modal-dialog modal-lg modal-dialog-centered">
							<div className="modal-content">
								<div className="modal-header">
									<h5 className="modal-title">{this.state.modalTitle}</h5>
									<button
										type="button"
										className="btn-close"
										data-bs-dismiss="modal"
										aria-label="Close"></button>
								</div>
								<div className="modal-body">
									{/* Inputs for item properties
                                    value: assigns data from component state
                                    onChange: calls specified function to update state */}
									<div className="input-group mb-3">
										<span className="input-group-text">Subscription duration in days</span>
										<input
											type="text"
											className="form-control"
											value={this.state.premiumDays}
											onChange={this.changeDays}
										/>
									</div>
									<div className="input-group mb-3">
										<span className="input-group-text">Subscription price</span>
										<input
											type="text"
											className="form-control"
											value={this.state.premiumPrice}
											onChange={this.changePrice}
										/>
									</div>
									{/* If selected item id == 0 Than we need to add new item */}
									{this.state.premiumID === 0 ? (
										<button
											type="button"
											className="btn btn-primary float-start"
											onClick={() => this.createClick()}>
											Create
										</button>
									) : null}
									{/* If selected item id !== 0 Than we need to updating existing item */}
									{this.state.premiumID !== 0 ? (
										<button
											type="button"
											className="btn btn-primary float-start"
											onClick={() => this.updateClick()}>
											Update
										</button>
									) : null}
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		);
	}
}