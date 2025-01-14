namespace View.Sdk
{
    using System;

    /// <summary>
    /// API error response.
    /// </summary>
    public class ApiErrorResponse
    {
        #region Public-Members

        /// <summary>
        /// Error.
        /// </summary>
        public ApiErrorEnum Error { get; set; } = ApiErrorEnum.AuthenticationFailed;

        /// <summary>
        /// Human-readable message.
        /// </summary>
        public string Message
        {
            get
            {
                switch (Error)
                {
                    case ApiErrorEnum.AuthenticationFailed:
                        return "Your authentication material was not accepted.";
                    case ApiErrorEnum.AuthorizationFailed:
                        return "Your authentication material was accepted, but you are not authorized to perform this request.";
                    case ApiErrorEnum.BadGateway:
                        return "Your request is unable to be serviced as there are no origin servers available.";
                    case ApiErrorEnum.BadRequest:
                        return "We were unable to discern your request.  Please check your URL, query, and request body.";
                    case ApiErrorEnum.Conflict:
                        return "Operation failed as it would create a conflict with an existing resource.";
                    case ApiErrorEnum.DeserializationError:
                        return "Your request body was invalid and could not be deserialized.";
                    case ApiErrorEnum.Inactive:
                        return "Your account, credentials, or the requested resource are marked as inactive.";
                    case ApiErrorEnum.InternalError:
                        return "An internal error has been encountered.";
                    case ApiErrorEnum.InvalidRange:
                        return "An invalid range has been supplied and cannot be fulfilled.";
                    case ApiErrorEnum.InUse:
                        return "The requested resource is in use.";
                    case ApiErrorEnum.NotEmpty:
                        return "The requested resource is not empty.";
                    case ApiErrorEnum.NotFound:
                        return "The requested resource was not found.";
                    case ApiErrorEnum.Timeout:
                        return "The request was not completed within the specified timeout interval.";
                    case ApiErrorEnum.TooLarge:
                        return "The size of your request exceeds the maximum allowed by this server.";

                    case ApiErrorEnum.NoTypeDetectorConnectivity:
                        return "Unable to establish a connection to the type detector.";
                    case ApiErrorEnum.UnknownTypeDetected:
                        return "An unrecognizable data type was supplied.";

                    case ApiErrorEnum.PdfConversionFailed:
                        return "The supplied object was unable to be converted to PDF.";

                    case ApiErrorEnum.NoGraphConnectivity:
                        return "Unable to establish a connection to the graph repository.";
                    case ApiErrorEnum.GraphOperationFailed:
                        return "An operation against the graph repository has failed.";

                    case ApiErrorEnum.NoUdrConnectivity:
                        return "Unable to establish a connection to the UDR endpoint.";
                    case ApiErrorEnum.UdrGenerationFailed:
                        return "Unable to generate UDR document.";

                    case ApiErrorEnum.NoSemanticCellConnectivity:
                        return "Unable to establish a connection to the semantic cell extraction endpoint.";
                    case ApiErrorEnum.SemanticCellExtractionFailed:
                        return "Unable to extract semantic cells.";

                    case ApiErrorEnum.NoDataCatalogConnectivity:
                        return "Unable to establish a connection to the data catalog endpoint.";
                    case ApiErrorEnum.DataCatalogPersistFailed:
                        return "Unable to persist data within the data catalog.";
                    case ApiErrorEnum.UnknownDataCatalogType:
                        return "An unknown data catalog type was encountered.";

                    case ApiErrorEnum.NoEmbeddingsConnectivity:
                        return "Unable to establish a connection to the embeddings generation endpoint.";
                    case ApiErrorEnum.UnknownEmbeddingsGeneratorType:
                        return "An unknown embeddings generator type was encountered.";
                    case ApiErrorEnum.EmbeddingsGenerationFailed:
                        return "Unable to generate embeddings.";

                    case ApiErrorEnum.NoVectorConnectivity:
                        return "Unable to establish a connection to the vector repository.";
                    case ApiErrorEnum.UnknownVectorRepositoryType:
                        return "An unknown vector repository type was encountered.";
                    case ApiErrorEnum.VectorPersistFailed:
                        return "Unable to persist embeddings within the vectore store.";

                    case ApiErrorEnum.NoObjectMetadata:
                        return "No object metadata was supplied.";
                    case ApiErrorEnum.NoObjectData:
                        return "No object data was supplied.";
                    case ApiErrorEnum.NoMetadataRule:
                        return "No metadata rule was supplied.";
                    case ApiErrorEnum.RequestBodyMissing:
                        return "A request body is required for this operation.";
                    case ApiErrorEnum.RequiredPropertiesMissing:
                        return "A required property was missing from the request.";

                    default:
                        return "An unknown error code '" + Error.ToString() + "' was encountered.";
                }
            }
        }

        /// <summary>
        /// HTTP status code.
        /// </summary>
        public int StatusCode
        {
            get
            {
                switch (Error)
                {
                    case ApiErrorEnum.AuthenticationFailed:
                        return 401;
                    case ApiErrorEnum.AuthorizationFailed:
                        return 401;
                    case ApiErrorEnum.BadGateway:
                        return 502;
                    case ApiErrorEnum.BadRequest:
                        return 400;
                    case ApiErrorEnum.Conflict:
                        return 409;
                    case ApiErrorEnum.DeserializationError:
                        return 400;
                    case ApiErrorEnum.Inactive:
                        return 401;
                    case ApiErrorEnum.InternalError:
                        return 500;
                    case ApiErrorEnum.InvalidRange:
                        return 400;
                    case ApiErrorEnum.InUse:
                        return 409;
                    case ApiErrorEnum.NotEmpty:
                        return 400;
                    case ApiErrorEnum.NotFound:
                        return 404;
                    case ApiErrorEnum.Timeout:
                        return 408;
                    case ApiErrorEnum.TokenExpired:
                        return 401;
                    case ApiErrorEnum.TooLarge:
                        return 413;

                    case ApiErrorEnum.NoTypeDetectorConnectivity:
                        return 500;
                    case ApiErrorEnum.UnknownTypeDetected:
                        return 400;

                    case ApiErrorEnum.PdfConversionFailed:
                        return 500;

                    case ApiErrorEnum.NoUdrConnectivity:
                        return 500;
                    case ApiErrorEnum.UdrGenerationFailed:
                        return 500;
                    case ApiErrorEnum.NoDataCatalogConnectivity:
                        return 500;
                    case ApiErrorEnum.DataCatalogPersistFailed:
                        return 500;
                    case ApiErrorEnum.UnknownDataCatalogType:
                        return 400;

                    case ApiErrorEnum.NoEmbeddingsConnectivity:
                        return 500;
                    case ApiErrorEnum.UnknownEmbeddingsGeneratorType:
                        return 400;
                    case ApiErrorEnum.EmbeddingsGenerationFailed:
                        return 500;

                    case ApiErrorEnum.NoVectorConnectivity:
                        return 500;
                    case ApiErrorEnum.UnknownVectorRepositoryType:
                        return 400;
                    case ApiErrorEnum.VectorPersistFailed:
                        return 500;

                    case ApiErrorEnum.NoGraphConnectivity:
                        return 500;
                    case ApiErrorEnum.GraphOperationFailed:
                        return 500;

                    case ApiErrorEnum.NoObjectMetadata:
                        return 400;
                    case ApiErrorEnum.NoObjectData:
                        return 400;
                    case ApiErrorEnum.NoMetadataRule:
                        return 400;
                    case ApiErrorEnum.RequestBodyMissing:
                        return 400;
                    case ApiErrorEnum.RequiredPropertiesMissing:
                        return 400;

                    default:
                        return 500;
                }
            }
        }

        /// <summary>
        /// Additional contextual information.
        /// </summary>
        public object Context { get; set; } = null;

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public ApiErrorResponse()
        {

        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="error">Error code.</param>
        /// <param name="context">Context.</param>
        /// <param name="description">Description.</param>
        /// 
        public ApiErrorResponse(ApiErrorEnum error, object context = null, string description = null)
        {
            Error = error;
            Context = context;
            Description = description;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
