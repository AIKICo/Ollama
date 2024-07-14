
#nullable enable

namespace Ollama
{
    public partial class CompletionsClient
    {
        partial void PrepareGenerateCompletionArguments(
            global::System.Net.Http.HttpClient httpClient,
            global::Ollama.GenerateCompletionRequest request);
        partial void PrepareGenerateCompletionRequest(
            global::System.Net.Http.HttpClient httpClient,
            global::System.Net.Http.HttpRequestMessage httpRequestMessage,
            global::Ollama.GenerateCompletionRequest request);
        partial void ProcessGenerateCompletionResponse(
            global::System.Net.Http.HttpClient httpClient,
            global::System.Net.Http.HttpResponseMessage httpResponseMessage);

        /// <summary>
        /// Generate a response for a given prompt with a provided model.<br/>
        /// The final response object will include statistics and additional data from the request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        public async global::System.Collections.Generic.IAsyncEnumerable<global::Ollama.GenerateCompletionResponse> GenerateCompletionAsync(
            global::Ollama.GenerateCompletionRequest request,
            [global::System.Runtime.CompilerServices.EnumeratorCancellation] global::System.Threading.CancellationToken cancellationToken = default)
        {
            request = request ?? throw new global::System.ArgumentNullException(nameof(request));

            PrepareArguments(
                client: _httpClient);
            PrepareGenerateCompletionArguments(
                httpClient: _httpClient,
                request: request);

            using var httpRequest = new global::System.Net.Http.HttpRequestMessage(
                method: global::System.Net.Http.HttpMethod.Post,
                requestUri: new global::System.Uri(_httpClient.BaseAddress?.AbsoluteUri.TrimEnd('/') + "/generate", global::System.UriKind.RelativeOrAbsolute));
            var __json = global::System.Text.Json.JsonSerializer.Serialize(request, global::Ollama.SourceGenerationContext.Default.GenerateCompletionRequest);
            httpRequest.Content = new global::System.Net.Http.StringContent(
                content: __json,
                encoding: global::System.Text.Encoding.UTF8,
                mediaType: "application/json");

            PrepareRequest(
                client: _httpClient,
                request: httpRequest);
            PrepareGenerateCompletionRequest(
                httpClient: _httpClient,
                httpRequestMessage: httpRequest,
                request: request);

            using var response = await _httpClient.SendAsync(
                request: httpRequest,
                completionOption: global::System.Net.Http.HttpCompletionOption.ResponseHeadersRead,
                cancellationToken: cancellationToken).ConfigureAwait(false);

            ProcessResponse(
                client: _httpClient,
                response: response);
            ProcessGenerateCompletionResponse(
                httpClient: _httpClient,
                httpResponseMessage: response);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            using var reader = new global::System.IO.StreamReader(stream);

            while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
            {
                var __content = await reader.ReadLineAsync().ConfigureAwait(false) ?? string.Empty;
                var streamedResponse = global::System.Text.Json.JsonSerializer.Deserialize(__content, global::Ollama.SourceGenerationContext.Default.GenerateCompletionResponse) ??
                                       throw new global::System.InvalidOperationException($"Response deserialization failed for \"{__content}\" ");

                yield return streamedResponse;
            }
        }

        /// <summary>
        /// Generate a response for a given prompt with a provided model.<br/>
        /// The final response object will include statistics and additional data from the request.
        /// </summary>
        /// <param name="model">
        /// The model name. <br/>
        /// Model names follow a `model:tag` format. Some examples are `orca-mini:3b-q4_1` and `llama3:70b`. The tag is optional and, if not provided, will default to `latest`. The tag is used to identify a specific version.<br/>
        /// Example: llama3:8b
        /// </param>
        /// <param name="prompt">
        /// The prompt to generate a response.<br/>
        /// Example: Why is the sky blue?
        /// </param>
        /// <param name="images">
        /// (optional) a list of Base64-encoded images to include in the message (for multimodal models such as llava)
        /// </param>
        /// <param name="system">
        /// The system prompt to (overrides what is defined in the Modelfile).
        /// </param>
        /// <param name="template">
        /// The full prompt or prompt template (overrides what is defined in the Modelfile).
        /// </param>
        /// <param name="context">
        /// The context parameter returned from a previous request to [generateCompletion], this can be used to keep a short conversational memory.
        /// </param>
        /// <param name="options">
        /// Additional model parameters listed in the documentation for the Modelfile such as `temperature`.
        /// </param>
        /// <param name="format">
        /// The format to return a response in. Currently the only accepted value is json.<br/>
        /// Enable JSON mode by setting the format parameter to json. This will structure the response as valid JSON.<br/>
        /// Note: it's important to instruct the model to use JSON in the prompt. Otherwise, the model may generate large amounts whitespace.
        /// </param>
        /// <param name="raw">
        /// If `true` no formatting will be applied to the prompt and no context will be returned. <br/>
        /// You may choose to use the `raw` parameter if you are specifying a full templated prompt in your request to the API, and are managing history yourself.
        /// </param>
        /// <param name="stream">
        /// If `false` the response will be returned as a single response object, otherwise the response will be streamed as a series of objects.<br/>
        /// Default Value: true
        /// </param>
        /// <param name="keepAlive">
        /// How long (in minutes) to keep the model loaded in memory.<br/>
        /// - If set to a positive duration (e.g. 20), the model will stay loaded for the provided duration.<br/>
        /// - If set to a negative duration (e.g. -1), the model will stay loaded indefinitely.<br/>
        /// - If set to 0, the model will be unloaded immediately once finished.<br/>
        /// - If not set, the model will stay loaded for 5 minutes by default
        /// </param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        public async global::System.Collections.Generic.IAsyncEnumerable<global::Ollama.GenerateCompletionResponse> GenerateCompletionAsync(
            string model,
            string prompt,
            global::System.Collections.Generic.IList<string?>? images = default,
            string? system = default,
            string? template = default,
            global::System.Collections.Generic.IList<long>? context = default,
            global::Ollama.RequestOptions? options = default,
            global::Ollama.ResponseFormat? format = default,
            bool raw = default,
            bool stream = true,
            int? keepAlive = default,
            [global::System.Runtime.CompilerServices.EnumeratorCancellation] global::System.Threading.CancellationToken cancellationToken = default)
        {
            var request = new global::Ollama.GenerateCompletionRequest
            {
                Model = model,
                Prompt = prompt,
                Images = images,
                System = system,
                Template = template,
                Context = context,
                Options = options,
                Format = format,
                Raw = raw,
                Stream = stream,
                KeepAlive = keepAlive,
            };

            var enumerable = GenerateCompletionAsync(
                request: request,
                cancellationToken: cancellationToken);

            await foreach (var response in enumerable)
            {
                yield return response;
            }
        }
    }
}